using System;
using System.Xml.Linq;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.ProjectManagement;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace AudioWorks.Extensions
{
    sealed class ExtensionProjectContext : INuGetProjectContext
    {
        [NotNull] readonly ILogger _logger = LoggingManager.CreateLogger<ExtensionProjectContext>();

        public void Log(MessageLevel level, string message, [NotNull, ItemNotNull] params object[] args)
        {
            switch (level)
            {
                case MessageLevel.Info:
                    _logger.LogInformation(message, args);
                    break;
                case MessageLevel.Warning:
                    _logger.LogWarning(message, args);
                    break;
                case MessageLevel.Debug:
                    _logger.LogDebug(message, args);
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