/* Copyright © 2025 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.Management.Automation;
using Xunit;

namespace AudioWorks.Commands.Tests
{
    static class ExtensionMethods
    {
        internal static void WriteStreams(this ITestOutputHelper output, PowerShell ps)
        {
            foreach (var info in ps.Streams.Information)
                if (info.MessageData is string message)
                    output.WriteLine(message);

            foreach (var warning in ps.Streams.Warning)
                output.WriteLine($"Warning: {warning.Message}");
        }
    }
}
