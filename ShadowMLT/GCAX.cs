using System.Collections.Generic;
using System;
using ShadowMLT.Structures;
using System.Text;
using System.IO;

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
            GCAX gcax = new GCAX
            {
                mltFileName = mltFilePath,
                binFileName = binFilePath
            };

            byte[] binFile = File.ReadAllBytes(binFilePath);
            // byte[] mltFile = new byte[1024]; // TODO ^

            int numberOfEntries = BitConverterExtensions.ToInt32BigEndian(binFile, Bin.ENTRY_COUNT_OFFSET);

            gcax.bin.entryTable = new List<BinEntry>();
            gcax.bin.soundNames = new List<string>();
            //gcax.mlt.entryTable = new List<SoundEntry>();

            int positionIndex = 0x0;
            BinHeader binHeader = new BinHeader
            {
                unknown_0x0 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex),
                unknown_0x4 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x4),
                unknown_0x8 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x8),
                unknown_0xC = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0xC),
                unknown_0x10 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x10),
                unknown_0x14 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x14),
                unknown_0x18 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x18),
                unknown_0x1C = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x1C),
                unknown_0x20 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x20),
                numberOfEntries = numberOfEntries,
            };
            gcax.bin.header = binHeader;

            positionIndex = Bin.ENTRY_COUNT_OFFSET + 0x4;
            for (int i = 0; i < numberOfEntries; i++)
            {
                BinEntry entry = new BinEntry {
                    fileNameOffset = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex),
                    unknown0x4 = binFile[positionIndex + 0x4],
                    unknown0x5 = binFile[positionIndex + 0x5],
                    unknown0x6 = binFile[positionIndex + 0x6],
                    soundIndex = binFile[positionIndex + 0x7],
                    bankId = binFile[positionIndex + 0x8],
                    bankIdSoundIndex = binFile[positionIndex + 0x9],
                    loudnessLeftChannel = binFile[positionIndex + 0xA],
                    loudnessRightChannel = binFile[positionIndex + 0xB],
                    unknown0xC = binFile[positionIndex + 0xC],
                    soundDuration = binFile[positionIndex + 0xD],
                    unknown0xE_Divisible_By_3_Changes_Bank = binFile[positionIndex + 0xE],
                    unknown0xF = binFile[positionIndex + 0xF],
                    unknown0x10 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x10),
                    unknown0x14_NoSoundIfGreaterThan0x80 = binFile[positionIndex + 0x14],
                    unknown0x15 = binFile[positionIndex + 0x15],
                    unknown0x16 = binFile[positionIndex + 0x16],
                    unknown0x17 = binFile[positionIndex + 0x17],
                };
                gcax.bin.entryTable.Add(entry);

                var stringPositionIndex = Bin.SOUND_NAMES_ADDITIVE_OFFSET + entry.fileNameOffset;
                StringBuilder soundName = new StringBuilder();
                while (stringPositionIndex < binFile.Length)
                {
                    var letter = (char)binFile[stringPositionIndex];
                    soundName.Append(letter);
                    if (letter == '\0')
                        break;
                    stringPositionIndex++;
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

    public static class BitConverterExtensions
    {
        public static int ToInt32BigEndian(byte[] bytes, int startIndex)
        {
            Array.Reverse(bytes, startIndex, sizeof(int));
            return BitConverter.ToInt32(bytes, startIndex);
        }
    }
}
