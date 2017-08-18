using System.IO;
using System.Text;

namespace AudioWorks.Extensions.Wave
{
    class RiffReader : BinaryReader
    {
        public RiffReader(Stream input)
            : base(input, Encoding.ASCII, true)
        {
        }

        internal void Initialize()
        {
            if (new string(ReadChars(4)) != "RIFF")
                throw new IOException("Not a valid RIFF stream.");
        }
    }
}
