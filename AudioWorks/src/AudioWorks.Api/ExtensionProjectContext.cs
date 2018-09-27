/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Xml.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.ProjectManagement;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AudioWorks.Api
{
    sealed class ExtensionProjectContext : INuGetProjectContext
    {
        [NotNull] readonly ILogger _logger = LoggerManager.LoggerFactory.CreateLogger<ExtensionProjectContext>();

        public void Log(MessageLevel level, [CanBeNull] string message, [NotNull, ItemNotNull] params object[] args)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (level)
            {
                case MessageLevel.Debug:
                    _logger.LogTrace(message, args);
                    break;
                case MessageLevel.Info:
                    _logger.LogDebug(message, args);
                    break;
                case MessageLevel.Warning:
                    _logger.LogWarning(message, args);
                    break;
                case MessageLevel.Error:
                    _logger.LogError(message, args);
                    break;
            }
        }

        public void ReportError([NotNull] string message)
        {
            _logger.LogError(message);
        }

        public FileConflictAction ResolveFileConflict([CanBeNull] string message) => FileConflictAction.Overwrite;

        [CanBeNull]
        public PackageExtractionContext PackageExtractionContext { get; set; } = new PackageExtractionContext(
            PackageSaveMode.Defaultv3,
            XmlDocFileSaveMode.Skip,
            NullLogger.Instance,
            null,
            null);

        [CanBeNull]
        public ISourceControlManagerProvider SourceControlManagerProvider => null;

        [CanBeNull]
        public ExecutionContext ExecutionContext => null;

        [CanBeNull]
        public XDocument OriginalPackagesConfig { get; set; }

        public NuGetActionType ActionType { get; set; }

        public Guid OperationId { get; set; }
    }
}