using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;
using AudioWorks.Api;
using JetBrains.Annotations;

namespace AudioWorks.Commands
{
    /// <summary>
    /// An <see cref="IArgumentCompleter"/> for propogating the names of all available encoders.
    /// </summary>
    /// <seealso cref="IArgumentCompleter"/>
    public sealed class EncoderCompleter : IArgumentCompleter
    {
        /// <summary>
        /// Completes the argument with the available encoder names.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="wordToComplete">The word to complete.</param>
        /// <param name="commandAst">The command ast.</param>
        /// <param name="fakeBoundParameters">The fake bound parameters.</param>
        /// <returns>The available encoder names.</returns>
        [NotNull]
        public IEnumerable<CompletionResult> CompleteArgument(
            string commandName,
            string parameterName,
            string wordToComplete,
            CommandAst commandAst,
            IDictionary fakeBoundParameters)
        {
            return AudioEncoderManager.GetEncoderNames().Select(name => new CompletionResult(name));
        }
    }
}