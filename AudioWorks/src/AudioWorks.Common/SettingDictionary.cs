/* Copyright © 2018 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AudioWorks.Common
{
    /// <summary>
    /// Represents a dictionary of settings which you can pass to various methods.
    /// </summary>
    /// <seealso cref="Dictionary{String, Object}"/>
    public class SettingDictionary : IDictionary<string, object>
    {
        readonly IDictionary<string, object> _dictionary = new Dictionary<string, object>();

        /// <summary>
        /// Gets the value associated with the specified key and of the specified type.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>true, if the value is present in the dictionary. Otherwise, false.</returns>
        public bool TryGetValue<TValue>(string key, [MaybeNullWhen(false)] out TValue value)
        {
            if (TryGetValue(key, out var objectValue) && objectValue is TValue typedValue)
            {
                value = typedValue;
                return true;
            }

            value = default;
            return false;
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _dictionary).GetEnumerator();

        /// <inheritdoc/>
        public virtual void Add(KeyValuePair<string, object> item) => _dictionary.Add(item);

        /// <inheritdoc/>
        public void Clear() => _dictionary.Clear();

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<string, object> item) => _dictionary.Contains(item);

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) =>
            _dictionary.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<string, object> item) => _dictionary.Remove(item);

        /// <inheritdoc/>
        public int Count => _dictionary.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => _dictionary.IsReadOnly;

        /// <inheritdoc/>
        public virtual void Add(string key, object value)
        {
            ArgumentNullException.ThrowIfNull(nameof(value));
            _dictionary.Add(key, value);
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key) => _dictionary.ContainsKey(key);

        /// <inheritdoc/>
        public bool Remove(string key) => _dictionary.Remove(key);

        /// <inheritdoc/>
        public bool TryGetValue(string key, out object value) => _dictionary.TryGetValue(key, out value!);

        /// <inheritdoc/>
        public virtual object this[string key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc/>
        public ICollection<string> Keys => _dictionary.Keys;

        /// <inheritdoc/>
        public ICollection<object> Values => _dictionary.Values;
    }
}