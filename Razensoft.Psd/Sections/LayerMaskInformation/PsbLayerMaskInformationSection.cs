namespace Razensoft.Psd.Sections.LayerMaskInformation
{
    internal class PsbLayerMaskInformationSection : LayerMaskInformationSection
    {
        public PsbLayerMaskInformationSection(long length, byte[] data)
        {
            Length = length;
            Data = data;
        }

        public long Length { get; }

        public byte[] Data { get; }
    }
}
