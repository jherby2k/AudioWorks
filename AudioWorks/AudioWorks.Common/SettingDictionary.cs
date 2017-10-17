using JetBrains.Annotations;
using System.Collections.Generic;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a dictionary of settings which you can pass to various methods.
    /// </summary>
    /// <seealso cref="Dictionary{String, Object}"/>
    [PublicAPI]
    public class SettingDictionary : Dictionary<string, object>
    {
    }
}