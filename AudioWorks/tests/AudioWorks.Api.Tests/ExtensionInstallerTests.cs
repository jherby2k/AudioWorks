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
            var extensionRoot = new DirectoryInfo(ExtensionInstaller.ExtensionRoot);

            if (!ExtensionInstaller.LoadComplete && extensionRoot.Exists)
                extensionRoot.Delete(true);
            await ExtensionInstaller.InstallAsync().ConfigureAwait(true);
            extensionRoot.Refresh();

            Assert.True(extensionRoot.Exists);
            Assert.True(extensionRoot.GetDirectories().Length > 0);
            Assert.All(extensionRoot.GetDirectories(), extensionDir =>
                Assert.True(extensionDir.GetFiles().Length > 0));
        }

        [Fact(DisplayName = "ExtensionInstaller's InstallAsync method removes unpublished extensions")]
        public async void InstallAsyncRemovesUnpublishedExtensions()
        {
            var extensionRoot = new DirectoryInfo(ExtensionInstaller.ExtensionRoot);

            if (!extensionRoot.Exists)
                extensionRoot.Create();
            var fakeExtensionDir = extensionRoot.CreateSubdirectory("AudioWorks.Extensions.OldExtension.1.0.0");
            using (File.Create(Path.Combine(fakeExtensionDir.FullName, "AudioWorks.Extensions.OldExtension.dll")))
            {
            }
            await ExtensionInstaller.InstallAsync().ConfigureAwait(true);
            fakeExtensionDir.Refresh();

            Assert.False(fakeExtensionDir.Exists);
        }
    }
}
