using System;
using System.Xml.Linq;
using JetBrains.Annotations;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.ProjectManagement;

namespace AudioWorks.Extensions
{
    sealed class ExtensionProjectContext : INuGetProjectContext
    {
        [NotNull] readonly ILogger _logger;

        public ExtensionProjectContext([NotNull] ILogger logger)
        {
            _logger = logger;

            PackageExtractionContext =
                new PackageExtractionContext(PackageSaveMode.Defaultv3, XmlDocFileSaveMode.Skip, logger, null);
        }

        public void Log(MessageLevel level, string message, [CanBeNull, ItemCanBeNull] params object[] args)
        {
            switch (level)
            {
                case MessageLevel.Info:
                    _logger.LogInformation(message);
                    break;
                case MessageLevel.Warning:
                    _logger.LogWarning(message);
                    break;
                case MessageLevel.Debug:
                    _logger.LogDebug(message);
                    break;
                case MessageLevel.Error:
                    _logger.LogError(message);
                    break;
            }
        }

        public void ReportError([NotNull] string message)
        {
            _logger.LogError(message);
        }

        public FileConflictAction ResolveFileConflict([CanBeNull] string message) => FileConflictAction.Overwrite;

        [CanBeNull]
        public PackageExtractionContext PackageExtractionContext { get; set; }

        [CanBeNull]
        public ISourceControlManagerProvider SourceControlManagerProvider { get; }

        [CanBeNull]
        public ExecutionContext ExecutionContext { get; }

        [CanBeNull]
        public XDocument OriginalPackagesConfig { get; set; }

        public NuGetActionType ActionType { get; set; }

        public Guid OperationId { get; set; }
    }
}