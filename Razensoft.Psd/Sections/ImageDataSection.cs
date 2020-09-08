namespace Razensoft.Psd.Sections
{
    internal class ImageDataSection
    {
        public ImageDataSection(short compressionMethod, byte[] data)
        {
            CompressionMethod = compressionMethod;
            Data = data;
        }
        
        public short CompressionMethod { get; }
        
        public byte[] Data { get; }
    }
}
