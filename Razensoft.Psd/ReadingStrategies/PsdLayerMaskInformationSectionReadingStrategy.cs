using Razensoft.Psd.Sections.LayerMaskInformation;

namespace Razensoft.Psd.ReadingStrategies
{
    internal class PsdLayerMaskInformationSectionReadingStrategy : SectionReadingStrategy<LayerMaskInformationSection>
    {
        public override LayerMaskInformationSection Read(PhotoshopFileReader reader, PhotoshopFile photoshopFile)
        {
            var length = reader.ReadInt32();
            var data = reader.ReadBytes(length);
            return new PsdLayerMaskInformationSection(length, data);
        }
    }
}
