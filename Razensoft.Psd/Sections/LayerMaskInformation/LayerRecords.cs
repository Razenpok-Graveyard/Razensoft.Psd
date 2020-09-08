namespace Razensoft.Psd.Sections.LayerMaskInformation
{
    internal class LayerRecords
    {
        public RectInt32 Bounds { get; }

        public short ChannelCount { get; }

        public byte[] ChannelInformation { get; }

        public string Signature { get; }

        public string BlendModeKey { get; }

        public byte Opacity { get; }

        public byte Clipping { get; }

        public byte Flags { get; }

        public byte Filler { get; }

        public byte[] ExtraData { get; }
    }
}
