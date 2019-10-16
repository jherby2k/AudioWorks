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
using AudioWorks.Common;
using AudioWorks.Extensibility;

namespace AudioWorks.Api
{
    static class ExtensionMethods
    {
        internal static void ProcessSamples(
            this ISampleProcessor sampleProcessor,
            string inputFilePath,
            IProgress<int>? progress,
            CancellationToken cancellationToken)
        {
            using (var inputStream = File.OpenRead(inputFilePath))
            {
                // Try each decoder that supports this file extension
                foreach (var decoderFactory in ExtensionProviderWrapper.GetFactories<IAudioDecoder>(
                    "Extension", Path.GetExtension(inputFilePath)))
                    try
                    {
                        using (var decoderExport = decoderFactory.CreateExport())
                        {
                            decoderExport.Value.Initialize(inputStream);

                            while (!decoderExport.Value.Finished)
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                using (var samples = decoderExport.Value.DecodeSamples())
                                {
                                    sampleProcessor.Submit(samples);
                                    progress?.Report(samples.Frames);
                                }
                            }
                        }

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
