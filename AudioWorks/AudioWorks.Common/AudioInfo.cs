using JetBrains.Annotations;

namespace AudioWorks.Common
{
    [PublicAPI]
    public class AudioInfo
    {
        [NotNull]
        public string Description { get; }

        public AudioInfo(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new System.ArgumentNullException(nameof(description));
            }
        }
    }
}
