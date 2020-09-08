namespace Razensoft.Psd.Sections.ImageResources
{
    internal class ImageResourcesSection
    {
        public ImageResourcesSection(int length, byte[] data)
        {
            Length = length;
            Data = data;
        }

        public int Length { get; }

        public byte[] Data { get; }
    }
}
