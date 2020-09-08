using Razensoft.Psd.Sections;

namespace Razensoft.Psd.ReadingStrategies
{
    internal class ColorModeDataSectionReadingStrategy : SectionReadingStrategy<ColorModeDataSection>
    {
        public override ColorModeDataSection Read(PhotoshopFileReader reader, PhotoshopFile photoshopFile)
        {
            var length = reader.ReadInt32();
            var data = reader.ReadBytes(length);
            return new ColorModeDataSection(length, data);
        }
    }
}
