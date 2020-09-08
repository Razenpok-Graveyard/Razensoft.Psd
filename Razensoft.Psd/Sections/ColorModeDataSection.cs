namespace Razensoft.Psd.Sections
{
    internal class ColorModeDataSection
    {
        public ColorModeDataSection(int length, byte[] colorData)
        {
            Length = length;
            ColorData = colorData;
        }

        public int Length { get; }

        public byte[] ColorData { get; }
    }
}
