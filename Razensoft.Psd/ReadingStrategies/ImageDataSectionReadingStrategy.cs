using System;
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
            if (compressionMethod == 0)
            {
                for (var i = 0; i < channelCount; i++)
                {
                    dataLength += pitch * height;
                }
            }
            else if (compressionMethod == 1)
            {
                for (var i = 0; i < channelCount; i++)
                {
                    var lengths = new int[height];
                    for (var j = 0; i < lengths.Length; i++)
                    {
                        if (photoshopFile.Version == 1)
                        {
                            dataLength += reader.ReadInt16();
                        }
                        else
                        {
                            dataLength += reader.ReadInt32();
                        }
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            var data = reader.ReadBytes(dataLength);
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
    }
}
