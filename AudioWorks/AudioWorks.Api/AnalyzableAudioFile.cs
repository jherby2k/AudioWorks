using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AudioWorks.Api
{
    /// <summary>
    /// Represents a single track of audio on the filesystem that can be analyzed in order to set additional metadata.
    /// </summary>
    /// <seealso cref="TaggedAudioFile"/>
    /// <seealso cref="IAnalyzableAudioFile"/>
    [PublicAPI]
    [Serializable]
    public sealed class AnalyzableAudioFile : TaggedAudioFile, IAnalyzableAudioFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzableAudioFile"/> class.
        /// </summary>
        /// <param name="path">The fully-qualified path to the file.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="path"/> is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown if <paramref name="path"/> does not exist.</exception>
        public AnalyzableAudioFile([NotNull] string path)
            : base(path)
        {
        }

        /// <inheritdoc />
        public async Task AnalyzeAsync(string analyzer, SettingDictionary settings, GroupToken groupToken, CancellationToken cancellationToken)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            var analyzerFactory = ExtensionProvider.GetFactories<IAudioAnalyzer>("Name", analyzer).SingleOrDefault();
            if (analyzerFactory == null)
                throw new ArgumentException($"No '{analyzer}' analyzer is available.", nameof(analyzer));

            using (var fileStream = File.OpenRead(Path))
            using (var analyzerExport = analyzerFactory.CreateExport())
            {
                var analyzerInstance = analyzerExport.Value;
                analyzerInstance.Initialize(Info, settings ?? new SettingDictionary(), groupToken);

                // Try each decoder that supports this file extension:
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioDecoder>(
                    "Extension", System.IO.Path.GetExtension(Path)))
                    try
                    {
                        using (var decoderExport = decoderFactory.CreateExport())
                        {
                            decoderExport.Value.Initialize(fileStream);

                            var block = new ActionBlock<SampleCollection>(samples => analyzerInstance.Submit(samples),
                                new ExecutionDataflowBlockOptions { SingleProducerConstrained = true });
                            while (!decoderExport.Value.Finished)
                                await block.SendAsync(decoderExport.Value.DecodeSamples()).ConfigureAwait(false);
                            block.Complete();
                            await block.Completion.ConfigureAwait(false);

                            CopyFields(analyzerExport.Value.GetResult(), Metadata);
                            CopyFields(analyzerExport.Value.GetGroupResult(), Metadata);
                            return;
                        }
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another:
                        fileStream.Position = 0;
                    }
            }

            throw new AudioUnsupportedException("No supporting decoders are available.");
        }

        static void CopyFields([NotNull] AudioMetadata source, [NotNull] AudioMetadata destination)
        {
            // Copy every non-blank field from source to destination
            foreach (var property in typeof(AudioMetadata).GetProperties())
            {
                var value = property.GetValue(source);
                if (!string.IsNullOrEmpty((string) value))
                    property.SetValue(destination, value);
            }
        }
    }
}