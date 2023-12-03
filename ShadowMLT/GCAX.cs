using System.Collections.Generic;
using System;
using ShadowMLT.Structures;
using System.Text;
using System.IO;
using System.ComponentModel;

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

            int numberOfEntries = BitConverterExtensions.ToInt32BigEndian(binFile, Bin.ENTRY_COUNT_OFFSET);

            gcax.bin.entryTable = new List<BinEntry>();
            gcax.bin.soundNames = new List<string>();
            gcax.mlt.entryTable = new List<SoundEntry>();

            int positionIndex = 0x0;
            BinHeader binHeader = new BinHeader
            {
                unknown0x0 = binFile[positionIndex],
                unknown0x1 = binFile[positionIndex + 0x1],
                unknown0x2 = binFile[positionIndex + 0x2],
                unknown0x3 = binFile[positionIndex + 0x3],
                unknown0x4 = binFile[positionIndex + 0x4],
                unknown0x5 = binFile[positionIndex + 0x5],
                unknown0x6 = binFile[positionIndex + 0x6],
                unknown0x7 = binFile[positionIndex + 0x7],
                unknown0x8 = binFile[positionIndex + 0x8],
                unknown0x9 = binFile[positionIndex + 0x9],
                unknown0xA = binFile[positionIndex + 0xA],
                unknown0xB = binFile[positionIndex + 0xB],
                unknown0xC = binFile[positionIndex + 0xC],
                unknown0xD = binFile[positionIndex + 0xD],
                unknown0xE = binFile[positionIndex + 0xE],
                unknown0xF = binFile[positionIndex + 0xF],
                unknown0x10 = binFile[positionIndex + 0x10],
                unknown0x11 = binFile[positionIndex + 0x11],
                unknown0x12 = binFile[positionIndex + 0x12],
                unknown0x13 = binFile[positionIndex + 0x13],
                unknown0x14 = binFile[positionIndex + 0x14],
                unknown0x15 = binFile[positionIndex + 0x15],
                unknown0x16 = binFile[positionIndex + 0x16],
                unknown0x17 = binFile[positionIndex + 0x17],
                unknown0x18 = binFile[positionIndex + 0x18],
                unknown0x19 = binFile[positionIndex + 0x19],
                unknown0x1A = binFile[positionIndex + 0x1A],
                unknown0x1B = binFile[positionIndex + 0x1B],
                unknown0x1C = binFile[positionIndex + 0x1C],
                unknown0x1D = binFile[positionIndex + 0x1D],
                unknown0x1E = binFile[positionIndex + 0x1E],
                unknown0x1F = binFile[positionIndex + 0x1F],
                unknown0x20 = binFile[positionIndex + 0x20],
                unknown0x21 = binFile[positionIndex + 0x21],
                unknown0x22 = binFile[positionIndex + 0x22],
                unknown0x23 = binFile[positionIndex + 0x23],
                /*                unknown_0x0 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex),
                                unknown_0x4 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x4),
                                unknown_0x8 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x8),
                                unknown_0xC = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0xC),
                                unknown_0x10 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x10),
                                unknown_0x14 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x14),
                                unknown_0x18 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x18),
                                unknown_0x1C = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x1C),
                                unknown_0x20 = BitConverterExtensions.ToInt32BigEndian(binFile, positionIndex + 0x20),*/
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
                    unknown0x10 = binFile[positionIndex + 0x10],
                    unknown0x11 = binFile[positionIndex + 0x11],
                    unknown0x12 = binFile[positionIndex + 0x12],
                    unknown0x13 = binFile[positionIndex + 0x13],
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

            FileStream mltFile = File.OpenRead(mltFilePath);
            BinaryReader reader = new BinaryReader(mltFile);
            
            gcax.mlt.header = reader.ReadBytes(Mlt.MLT_HEADER_SIZE);
            byte[] audioData = new byte[0x80000];

            byte[] mltAudioEntriesSignature = { 0x67, 0x63, 0x61, 0x78, 0x4D, 0x50, 0x42, 0x50 };
            positionIndex = 0;
            while (true)
            {
                var savedPosition = reader.BaseStream.Position;
                var signature = reader.ReadBytes(0x8);
                reader.BaseStream.Position = savedPosition;
                if (signature[0] == mltAudioEntriesSignature[0] && signature[1] == mltAudioEntriesSignature[1] && signature[2] == mltAudioEntriesSignature[2] && signature[3] == mltAudioEntriesSignature[3] && signature[4] == mltAudioEntriesSignature[4] && signature[5] == mltAudioEntriesSignature[5] && signature[6] == mltAudioEntriesSignature[6] && signature[7] == mltAudioEntriesSignature[7])
                    break;
                reader.Read(audioData, positionIndex, 1);
                positionIndex++;
            }
            byte[] finalAudioData = new byte[reader.BaseStream.Position - Mlt.MLT_HEADER_SIZE];
            gcax.mlt.audioData = finalAudioData;
            //gcax.mlt.entryTable = soundEntries;

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
