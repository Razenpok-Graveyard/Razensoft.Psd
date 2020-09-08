using JetBrains.Annotations;

namespace Razensoft.Psd
{
    [PublicAPI]
    public enum ColorMode : short
    {
        Bitmap = 0,
        Grayscale = 1,
        Indexed = 2,
        Rgb = 3,
        Cmyk = 4,
        Multichannel = 7,
        Duotone = 8,
        Lab = 9
    }
}
