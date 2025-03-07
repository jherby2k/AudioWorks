/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.IO;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Api
{
    static class ExtensionMethods
    {
        const int _channelCapacity = 20;

        internal static async ValueTask ProcessSamples(
            this ISampleProcessor sampleProcessor,
            string inputFilePath,
            IProgress<int>? progress,
            CancellationToken cancellationToken)
        {
            var inputStream = File.OpenRead(inputFilePath);
            await using (inputStream.ConfigureAwait(false))
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProviderWrapper.GetFactories<IAudioDecoder>(
                             "Extension", Path.GetExtension(inputFilePath)))
                    try
                    {
                        using (var decoderExport = decoderFactory.CreateExport())
                        {
                            var channel = Channel.CreateBounded<SampleBuffer>(
                                new BoundedChannelOptions(_channelCapacity) { SingleReader = true, SingleWriter = true });

                            var consumer = ConsumeAsync(channel.Reader, sampleProcessor, progress, cancellationToken);
                            await ProduceAsync(channel.Writer, inputStream, decoderExport.Value, cancellationToken)
                                .ConfigureAwait(false);
                            await consumer.ConfigureAwait(false);
                        }

                        return;
                    }
                    catch (AudioUnsupportedException)
                    {
                        // If a decoder wasn't supported, rewind the stream and try another
                        inputStream.Position = 0;
                    }

                throw new AudioUnsupportedException("No supporting decoders are available.");
            }
        }

        static async ValueTask ProduceAsync(
            ChannelWriter<SampleBuffer> writer,
            Stream inputStream,
            IAudioDecoder decoder,
            CancellationToken cancellationToken)
        {
            decoder.Initialize(inputStream);

            while (!decoder.Finished)
                await writer.WriteAsync(decoder.DecodeSamples(), cancellationToken)
                    .ConfigureAwait(false);

            writer.Complete();
        }

        static async ValueTask ConsumeAsync(
            ChannelReader<SampleBuffer> reader,
            ISampleProcessor sampleProcessor,
            IProgress<int>? progress,
            CancellationToken cancellationToken)
        {
            await foreach (var samples in reader.ReadAllAsync(cancellationToken)
                               .ConfigureAwait(false))
            {
                sampleProcessor.Submit(samples);
                samples.Dispose();
                progress?.Report(samples.Frames);
            }
        }
    }
}
