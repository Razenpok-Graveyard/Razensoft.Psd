using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using JetBrains.Annotations;
using Razensoft.Psd.Sections;

namespace Razensoft.Psd.ReadingStrategies
{
    internal class ImageDataSectionReadingStrategy : SectionReadingStrategy<ImageDataSection>
    {
        public override ImageDataSection Read(PhotoshopFileReader reader, PhotoshopFile photoshopFile)
        {
            var channelCount = photoshopFile.ChannelCount;
            var width = photoshopFile.Width;
            var height = photoshopFile.Height;
            var depth = photoshopFile.Depth;
            var compressionMethod = reader.ReadInt16();
            var dataLength = 0;
            var pitch = DepthToPitch(depth, width);
            byte[] data;
            if (compressionMethod == 0)
            {
                for (var i = 0; i < channelCount; i++)
                {
                    dataLength += pitch * height;
                }

                data = reader.ReadBytes(dataLength);
            }
            else if (compressionMethod == 1)
            {
                var dataBuffer = new List<byte>();
                var channelLengths = new List<int[]>();
                for (var i = 0; i < channelCount; i++)
                {
                    var lengths = new int[height];
                    for (var j = 0; j < height; j++)
                    {
                        lengths[j] = photoshopFile.Version == 1
                            ? reader.ReadInt16()
                            : reader.ReadInt32();
                    }

                    channelLengths.Add(lengths);
                }

                for (int i = 0; i < channelCount; i++)
                {
                    var lengths = channelLengths[i];
                    for (var j = 0; j < lengths.Length; j++)
                    {
                        var rleData = reader.ReadBytes(lengths[j]);
                        var decompressedData = new byte[width];
                        DecompressRleData(rleData, decompressedData);
                        dataBuffer.AddRange(decompressedData);
                    }
                }

                data = dataBuffer.ToArray();
            }
            else
            {
                throw new NotImplementedException();
            }

            return new ImageDataSection(compressionMethod, data);
        }

        private static int DepthToPitch(int depth, int width)
        {
            switch (depth)
            {
                case 1:
                    return width; //NOT Sure
                case 8:
                    return width;
                case 16:
                    return width * 2;
            }

            throw new NotSupportedException();
        }

        public static void DecompressRleData([NotNull] byte[] source, [NotNull] byte[] destination)
        {
            int index = 0;
            int num2 = 0;
            int num3 = 0;
            byte num4 = 0;
            int num5 = destination.Length;
            int num6 = source.Length;
            while ((num5 > 0) && (num6 > 0))
            {
                num3 = source[index++];
                num6--;
                if (num3 != 0x80)
                {
                    if (num3 > 0x80)
                    {
                        num3 -= 0x100;
                    }
                    if (num3 < 0)
                    {
                        num3 = 1 - num3;
                        if (num6 == 0)
                        {
                            throw new Exception("Input buffer exhausted in replicate");
                        }
                        if (num3 > num5)
                        {
                            throw new Exception(string.Format("Overrun in packbits replicate of {0} chars", num3 - num5));
                        }
                        num4 = source[index];
                        while (num3 > 0)
                        {
                            if (num5 == 0)
                            {
                                break;
                            }

                            destination[num2++] = num4;
                            num5--;
                            num3--;
                        }
                        if (num5 > 0)
                        {
                            index++;
                            num6--;
                        }
                        continue;
                    }
                    num3++;
                    while (num3 > 0)
                    {
                        if (num6 == 0)
                        {
                            throw new Exception("Input buffer exhausted in copy");
                        }
                        if (num5 == 0)
                        {
                            throw new Exception("Output buffer exhausted in copy");
                        }

                        destination[num2++] = source[index++];
                        num5--;
                        num6--;
                        num3--;
                    }
                }
            }
            if (num5 > 0)
            {
                for (num3 = 0; num3 < num6; num3++)
                {
                    destination[num2++] = 0;
                }
            }
        }
    }
}
