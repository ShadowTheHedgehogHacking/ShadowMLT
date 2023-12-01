namespace ShadowMLT.Structures
{
    /**
    ---Entries---
        0x0 | int String .bin offset | (first entry @ 0x28)
        0x4-0x7 | int?? Game SFX Index | 0x7 | byte? Game SFX Index
        0x8 | byte Bank ID
        0x9 | byte Bank SFX Index
        0xA | ?? (not sure)byte Loudness Left Channel
        0xB |  (99% sure) byte Loudness Right Channel
        0xC | ??
        0xD | byte SFX Duration
        0xE | ?? When divisible by 3 changes to menu sfx?
        0xF | ??
        0x10-0x13 | ??
        0x14 | ?? No Sound if greater than 0x80
        0x15 | ??
        0x16 | ??
        0x17 | ??
    **/
    public struct BinEntry
    {
        public int fileNameOffset;
        public byte unknown0x4;
        public byte unknown0x5;
        public byte unknown0x6;
        public byte soundIndex;
        public byte bankId;
        public byte bankIdSoundIndex;
        public byte loudnessLeftChannel;
        public byte loudnessRightChannel;
        public byte unknown0xC;
        public byte soundDuration;
        public byte unknown0xE_Divisible_By_3_Changes_Bank;
        public byte unknown0xF;
        public int unknown0x10;
        public byte unknown0x14_NoSoundIfGreaterThan0x80;
        public byte unknown0x15;
        public byte unknown0x16;
        public byte unknown0x17;
    }
}
