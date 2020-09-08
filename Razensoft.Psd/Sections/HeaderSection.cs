namespace Razensoft.Psd.Sections
{
    internal class HeaderSection
    {
        public HeaderSection(
            string signature,
            short version,
            byte[] reservedBytes,
            short channelCount,
            int height,
            int width,
            short depth,
            ColorMode colorMode)
        {
            Signature = signature;
            Version = version;
            ReservedBytes = reservedBytes;
            ChannelCount = channelCount;
            Height = height;
            Width = width;
            Depth = depth;
            ColorMode = colorMode;
        }

        public string Signature { get; }

        public short Version { get; }

        public byte[] ReservedBytes { get; }

        public short ChannelCount { get; }

        public int Height { get; }

        public int Width { get; }

        public short Depth { get; }

        public ColorMode ColorMode { get; }
    }
}
