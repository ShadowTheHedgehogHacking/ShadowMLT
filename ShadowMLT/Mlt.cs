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

        const int MLT_HEADER_SIZE = 0x60;
        const int DSP_HEADER_SIZE = 0x40;
        const int SOUND_ENTRY_SIZE = 0x50;

        public MltHeader header;
        public byte[] audioData;
        public List<SoundEntry> entryTable;

    }
}
