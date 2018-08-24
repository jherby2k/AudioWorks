#if !NETCOREAPP2_1
using System;
#endif
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
#if !NETCOREAPP2_1
using System.Runtime.InteropServices;
#endif
using System.Runtime.Loader;
using AudioWorks.Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace AudioWorks.Extensions
{
    sealed class ExtensionAssemblyResolver
    {
        readonly ILogger _logger = LoggingManager.LoggerFactory.CreateLogger<ExtensionAssemblyResolver>();

        [NotNull]
        internal Assembly Assembly { get; }

        internal ExtensionAssemblyResolver([NotNull] string path)
        {
            _logger.LogInformation("Loading extension '{0}'.", path);

            Assembly = Assembly.LoadFrom(path);
            var extensionDir = Path.GetDirectoryName(path);

            // Resolve dependencies from both the main and extension directories
            // ReSharper disable twice AssignNullToNotNullAttribute
            var assemblyFiles = Directory.GetFiles(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll")
                .Concat(Directory.GetFiles(extensionDir, "*.dll"));

#if NETCOREAPP2_1
            ResolveWithLoader(assemblyFiles);
        }
#else
            if (RuntimeInformation.FrameworkDescription.StartsWith(".NET Framework",
                StringComparison.Ordinal))
                ResolveFullFramework(assemblyFiles);
            else
                ResolveWithLoader(assemblyFiles);
        }

        void ResolveFullFramework([NotNull, ItemNotNull] IEnumerable<string> assemblyFiles)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (context, args) =>
            {
                var assemblyName = new AssemblyName(args.Name);

                _logger.LogInformation("Attempting to resolve a dependency on '{1}'.", assemblyName);

                var result = assemblyFiles
                    .Where(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                        AssemblyName.GetAssemblyName(assemblyFile), assemblyName))
                    .Select(Assembly.LoadFrom).FirstOrDefault();

                if (result != null)
                    _logger.LogInformation("Located dependency '{0}'.", result.FullName);
                else
                    _logger.LogCritical("Unable to locate dependency '{0}'.", assemblyName);

                return result;
            };
        }
#endif

        void ResolveWithLoader([NotNull, ItemNotNull] IEnumerable<string> assemblyFiles)
        {
            AssemblyLoadContext.Default.Resolving += (context, name) =>
            {
                _logger.LogDebug("Attempting to resolve a dependency on '{1}'.", name.FullName);

                var result = assemblyFiles
                    .Where(assemblyFile => AssemblyName.ReferenceMatchesDefinition(
                        AssemblyName.GetAssemblyName(assemblyFile),
                        new AssemblyName(name.Name)))
                    .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).FirstOrDefault();

                if (result != null)
                    _logger.LogDebug("Located dependency '{0}'.", result.FullName);
                else
                    _logger.LogDebug("Unable to locate dependency '{0}'.", name.FullName);

                return result;
            };
        }
    }
}
