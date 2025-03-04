/* Copyright © 2019 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System.IO;

namespace AudioWorks.TestUtilities
{
    public static class PathUtility
    {
        public static string GetMime(string path) =>
            Path.GetExtension(path) switch
            {
                ".wav" => "audio/wav",
                ".flac" => "audio/flac",
                ".m4a" => "audio/mp4",
                ".mp3" => "audio/mpeg",
                ".ogg" => "audio/ogg",
                ".opus" => "audio/ogg",
                _ => "application/octet-stream",
            };

        public static string GetTestFileRoot()
        {
            var rootDirectory = new DirectoryInfo(
                Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.Parent?.FullName;
            return rootDirectory == null
                ? throw new DirectoryNotFoundException("Unable to locate the test file root directory.")
                : Path.Combine(rootDirectory, "TestFiles");
        }
    }
}
