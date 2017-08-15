using JetBrains.Annotations;
using System.IO;

namespace AudioWorks.Api
{
    [PublicAPI]
    public static class AudioFileFactory
    {
        public static void Create([NotNull] string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("The file could not be found.", path);
        }
    }
}
