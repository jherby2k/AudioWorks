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
        public ExtensionProjectContext([NotNull] ILogger logger)
        {
            PackageExtractionContext =
                new PackageExtractionContext(PackageSaveMode.Defaultv3, XmlDocFileSaveMode.Skip, logger, null);
        }

        public void Log(MessageLevel level, string message, [CanBeNull, ItemCanBeNull] params object[] args)
        {
        }

        public void ReportError([CanBeNull] string message)
        {
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