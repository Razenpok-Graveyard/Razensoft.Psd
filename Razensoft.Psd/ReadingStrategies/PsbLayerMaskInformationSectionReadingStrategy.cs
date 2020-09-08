using Razensoft.Psd.Sections.LayerMaskInformation;

namespace Razensoft.Psd.ReadingStrategies
{
    internal class PsbLayerMaskInformationSectionReadingStrategy : SectionReadingStrategy<LayerMaskInformationSection>
    {
        public override LayerMaskInformationSection Read(PhotoshopFileReader reader, PhotoshopFile photoshopFile)
        {
            var length = reader.ReadInt64();
            // TODO: Don't just convert to int pls
            var data = reader.ReadBytes((int) length);
            return new PsbLayerMaskInformationSection(length, data);
        }
    }
}
