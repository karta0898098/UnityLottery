using System;

namespace RayFramework.Audio
{
    internal partial class AudioManager
    {
        private class AudioInfo
        {
            public DateTime LastUseTime { get; set; }
            public bool NerverRelease { get; set; }
            public object audioAsset { get; set; }
        }
    }
}
