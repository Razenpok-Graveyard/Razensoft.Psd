using System.Text;
using Razensoft.Psd.Sections;

namespace Razensoft.Psd.ReadingStrategies
{
    internal class HeaderSectionReadingStrategy : SectionReadingStrategy<HeaderSection>
    {
        public override HeaderSection Read(PhotoshopFileReader reader, PhotoshopFile photoshopFile)
        {
            var buffer = reader.ReadBytes(26);
            var bufferedReader = new PhotoshopFileReader(buffer);
            var signatureBytes = bufferedReader.ReadBytes(4);
            var signature = Encoding.ASCII.GetString(signatureBytes);
            var version = bufferedReader.ReadInt16();
            var reservedBytes = bufferedReader.ReadBytes(6);
            var channelCount = bufferedReader.ReadInt16();
            var height = bufferedReader.ReadInt32();
            var width = bufferedReader.ReadInt32();
            var depth = bufferedReader.ReadInt16();
            var colorMode = (ColorMode) bufferedReader.ReadInt16();
            return new HeaderSection(
                signature,
                version,
                reservedBytes,
                channelCount,
                height,
                width,
                depth,
                colorMode
            );
        }
    }
}
