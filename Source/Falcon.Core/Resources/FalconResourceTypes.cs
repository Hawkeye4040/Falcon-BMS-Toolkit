using System;

namespace Falcon.Resources
{
    [Serializable]
    public enum FalconResourceTypes : uint
    {
        Unknown = 0,
        ImageResource = 100, // 0x00000064
        SoundResource = 101, // 0x00000065
        FlatResource = 102 // 0x00000066
    }
}