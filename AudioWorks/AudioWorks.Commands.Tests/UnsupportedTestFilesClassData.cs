using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

namespace AudioWorks.Commands.Tests
{
    public class UnsupportedTestFilesClassData : IEnumerable<object[]>
    {
        [NotNull] readonly List<object[]> _data = new List<object[]>
        {
            new object[] { "Not Audio.txt" },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}