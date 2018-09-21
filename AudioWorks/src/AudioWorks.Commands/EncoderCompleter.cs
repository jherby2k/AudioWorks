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
    /// An <see cref="IArgumentCompleter"/> for propagating the names of all available encoders.
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
            var pattern = new WildcardPattern($"{wordToComplete}*", WildcardOptions.IgnoreCase);
            return AudioEncoderManager.GetEncoderInfo()
                .Where(info => pattern.IsMatch(info.Name))
                .Select(info => new CompletionResult(
                    info.Name,
                    info.Name,
                    CompletionResultType.ParameterValue,
                    info.Description));
        }
    }
}