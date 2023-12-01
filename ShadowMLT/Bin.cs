using ShadowMLT.Structures;
using System.Collections.Generic;

namespace ShadowMLT
{
    /**
    AudioData associated .bin (Ex sound\AudioDataPlayer.bin)

    ---HEADER--- BIG ENDIAN
    Size (num of entries) @ 0x24 (int)
    **/

    public struct Bin
    {
        public const int ENTRY_COUNT_OFFSET = 0x24;
        public const int ENTRY_SIZE = 0x20;
        public string fileName;
        public BinHeader header;
        public List<BinEntry> entryTable;
        public List<string> soundNames;
    }
}
