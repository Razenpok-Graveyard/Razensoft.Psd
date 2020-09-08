using Razensoft.Psd.Sections.ImageResources;

namespace Razensoft.Psd.ReadingStrategies
{
    internal class ImageResourcesSectionReadingStrategy : SectionReadingStrategy<ImageResourcesSection>
    {
        public override ImageResourcesSection Read(PhotoshopFileReader reader, PhotoshopFile photoshopFile)
        {
            var length = reader.ReadInt32();
            var data = reader.ReadBytes(length);
            return new ImageResourcesSection(length, data);
        }
    }
}
