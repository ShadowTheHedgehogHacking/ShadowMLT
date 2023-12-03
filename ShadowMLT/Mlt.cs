using ShadowMLT.Structures;
using System.Collections.Generic;

namespace ShadowMLT
{
    public struct Mlt
    {
        public const string headerFile = "gcaxMLT";
        public const string headerFileMono = "gcaxMLTM";

        public const string headerSoundDataUnknown = "gcaxMPB";
        public const string headerSoundData = "gcaxMPBW";

        public const string headerSoundEntries = "gcaxMPBP";

        // end of file is padded with 0x55s. Then at the very end of the file is an entire 0x10 of 0x55s

        public const int MLT_HEADER_SIZE = 0x60;
        public const int DSP_HEADER_SIZE = 0x40;
        public const int SOUND_ENTRY_SIZE = 0x50;

        public byte[] header;
        public byte[] audioData;
        public List<SoundEntry> entryTable;

    }
}
