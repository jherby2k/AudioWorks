using System.Collections.Generic;
using JetBrains.Annotations;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a dictionary of settings which you can pass to various methods.
    /// </summary>
    /// <seealso cref="Dictionary{String, Object}"/>
    [PublicAPI]
    public class SettingDictionary : Dictionary<string, object>
    {
        /// <summary>
        /// Gets the value associated with the specified key and of the specified type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true, if the value is present in the dictionary. Otherwise, false.</returns>
        public bool TryGetValue<TValue>([NotNull] string key, [CanBeNull] out TValue value)
        {
            if (TryGetValue(key, out var objectValue) && objectValue is TValue)
            {
                value = (TValue)objectValue;
                return true;
            }

            value = default;
            return false;
        }
    }
}