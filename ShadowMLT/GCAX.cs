using System.Collections.Generic;
using System;
using ShadowMLT.Structures;
using System.Text;

namespace ShadowMLT
{
    public struct GCAX
    {
        public string mltFileName;
        public string binFileName;
        public Mlt mlt;
        public Bin bin;

        public GCAX(string mltFilePath, string binFilePath) { this = ParseMLTandBIN(mltFilePath, binFilePath); }

        public static GCAX ParseMLTandBIN(string mltFilePath, string binFilePath)
        {
            GCAX gcax = new GCAX(mltFilePath, binFilePath);

            byte[] binFile = new byte[1024]; // TODO replace with reads
            // byte[] mltFile = new byte[1024]; // TODO ^

            int numberOfEntries = BitConverter.ToInt32(binFile, Bin.ENTRY_COUNT_OFFSET);
            
            gcax.bin.entryTable = new List<BinEntry>();
            gcax.bin.soundNames = new List<string>();
            //gcax.mlt.entryTable = new List<SoundEntry>();

            int positionIndex = 0x0;
            BinHeader binHeader = new BinHeader
            {
                unknown_0x0 = BitConverter.ToInt32(binFile, positionIndex),
                unknown_0x4 = BitConverter.ToInt32(binFile, positionIndex + 0x4),
                unknown_0x8 = BitConverter.ToInt32(binFile, positionIndex + 0x8),
                unknown_0xC = BitConverter.ToInt32(binFile, positionIndex + 0xC),
                unknown_0x10 = BitConverter.ToInt32(binFile, positionIndex + 0x10),
                unknown_0x14 = BitConverter.ToInt32(binFile, positionIndex + 0x14),
                unknown_0x18 = BitConverter.ToInt32(binFile, positionIndex + 0x18),
                unknown_0x1C = BitConverter.ToInt32(binFile, positionIndex + 0x1C),
                unknown_0x20 = BitConverter.ToInt32(binFile, positionIndex + 0x20),
                numberOfEntries = BitConverter.ToInt32(binFile, positionIndex + 0x24),
            };
            gcax.bin.header = binHeader;

            positionIndex = Bin.ENTRY_COUNT_OFFSET + 0x4;
            for (int i = 0; i < numberOfEntries; i++)
            {
                BinEntry entry = new BinEntry {
                    fileNameOffset = BitConverter.ToInt32(binFile, positionIndex),
                    soundIndex = BitConverter.ToInt32(binFile, positionIndex + 0x4),
                    bankId = binFile[positionIndex + 0x8],
                    bankIdSoundIndex = binFile[positionIndex + 0x9],
                    loudnessLeftChannel = binFile[positionIndex + 0xA],
                    loudnessRightChannel = binFile[positionIndex + 0xB],
                    unknown0xC = binFile[positionIndex + 0xC],
                    soundDuration = binFile[positionIndex + 0xD],
                    unknown0xE_Divisible_By_3_Changes_Bank = binFile[positionIndex + 0xE],
                    unknown0xF = binFile[positionIndex + 0xF],
                    unknown0x10 = BitConverter.ToInt32(binFile, positionIndex + 0x10),
                    unknown0x14_NoSoundIfGreaterThan0x80 = binFile[positionIndex + 0x14],
                    unknown0x15 = binFile[positionIndex + 0x15],
                    unknown0x16 = binFile[positionIndex + 0x16],
                    unknown0x17 = binFile[positionIndex + 0x17],
                };
                gcax.bin.entryTable.Add(entry);

                var stringPositionIndex = Bin.ENTRY_SIZE + entry.fileNameOffset;
                StringBuilder soundName = new StringBuilder();
                while (stringPositionIndex < binFile.Length)
                {
                    var letter = (char)binFile[stringPositionIndex];
                    soundName.Append(letter);
                    if (letter == '\0')
                        break;
                }
                gcax.bin.soundNames.Add(soundName.ToString());
                positionIndex += Bin.ENTRY_SIZE;
            }
            return gcax;
        }


        public byte[] ToBytes()
        {
            return new byte[1024];
        }
    }
}
