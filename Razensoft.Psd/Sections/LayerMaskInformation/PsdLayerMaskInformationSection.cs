namespace Razensoft.Psd.Sections.LayerMaskInformation
{
    internal class PsdLayerMaskInformationSection : LayerMaskInformationSection
    {
        public PsdLayerMaskInformationSection(int length, byte[] data)
        {
            Length = length;
            Data = data;
        }

        public int Length { get; }

        public byte[] Data { get; }
    }
}
