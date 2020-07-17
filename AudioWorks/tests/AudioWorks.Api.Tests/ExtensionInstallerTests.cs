/* Copyright © 2020 Jeremy Herbison

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
using AudioWorks.Common;
using AudioWorks.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace AudioWorks.Api.Tests
{
    public sealed class ExtensionInstallerTests
    {
        public ExtensionInstallerTests(ITestOutputHelper outputHelper) =>
            LoggerManager.AddSingletonProvider(() => new XunitLoggerProvider()).OutputHelper = outputHelper;

        [Fact(DisplayName = "ExtensionInstaller's InstallAsync method installs the available extensions")]
        public async void InstallAsyncInstallsExtensions()
        {
            var extensionRoot = new DirectoryInfo(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AudioWorks",
                "Extensions"));

            if (extensionRoot.Exists)
                extensionRoot.Delete(true);

            await ExtensionInstaller.InstallAsync().ConfigureAwait(true);

            Assert.True(extensionRoot.GetDirectories().Length == 1);
            Assert.True(extensionRoot.GetDirectories()[0].GetDirectories().Length > 0);
            Assert.All(extensionRoot.GetDirectories()[0].GetDirectories(),
                extensionDir => Assert.True(extensionDir.GetFiles().Length > 0));
        }
    }
}
