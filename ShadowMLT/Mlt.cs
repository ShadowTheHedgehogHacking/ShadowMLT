using ShadowMLT.Structures;
using System.Collections.Generic;

namespace ShadowMLT
{
    public struct Mlt
    {
        const string headerFile = "gcaxMLT";
        const string headerFileMono = "gcaxMLTM";

        const string headerSoundDataUnknown = "gcaxMPB";
        const string headerSoundData = "gcaxMPBW";
        const string headerSoundEntries = "gcaxMPBP";

        // end of file is padded with 0x55s. Then at the very end of the file is an entire 0x10 of 0x55s
        
        const int soundEntrySize = 0x40;
        const int soundEntrySizeWithUnknownSpacingBytes = 0x50;

        public List<SoundEntry> entryTable;

    }
}
