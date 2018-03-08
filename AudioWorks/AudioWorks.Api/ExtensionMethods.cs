using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using AudioWorks.Common;
using AudioWorks.Extensions;
using JetBrains.Annotations;

namespace AudioWorks.Api
{
    static class ExtensionMethods
    {
        internal static void ProcessSamples(
            [NotNull] this ISampleProcessor sampleProcessor,
            [NotNull] string inputFilePath,
            [CanBeNull] BlockingCollection<int> progressQueue,
            int minFramesToReport,
            CancellationToken cancellationToken)
        {
            using (var inputStream = File.OpenRead(inputFilePath))
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProvider.GetFactories<IAudioDecoder>(
                    "Extension", Path.GetExtension(inputFilePath)))
                    try
                    {
                        var framesSinceLastProgress = 0;

                        // Use an action block to process the sample collections asynchronously
                        var block = new ActionBlock<SampleCollection>(samples =>
                            {
                                sampleProcessor.Submit(samples);
                                samples.Return();

                                if (progressQueue == null) return;
                                if ((framesSinceLastProgress += samples.Frames) < minFramesToReport) return;
                                progressQueue.Add(framesSinceLastProgress, cancellationToken);
                                framesSinceLastProgress = 0;
                            },
                            new ExecutionDataflowBlockOptions
                            {
                                CancellationToken = cancellationToken,
                                SingleProducerConstrained = true
                            });

                        using (var decoderExport = decoderFactory.CreateExport())
                        {
                            decoderExport.Value.Initialize(inputStream);
                            while (!decoderExport.Value.Finished)
                                if (!block.Post(decoderExport.Value.DecodeSamples()))
                                    break;
                        }

                        block.Complete();
                        block.Completion.Wait(cancellationToken);

                        // Report any unreported frames
                        if (framesSinceLastProgress > 0)
                            progressQueue?.Add(framesSinceLastProgress, cancellationToken);

                        return;
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another:
                        inputStream.Position = 0;
                    }

                throw new AudioUnsupportedException("No supporting decoders are available.");
            }
        }
    }
}
