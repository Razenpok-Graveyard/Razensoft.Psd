using System.Linq;
using Razensoft.Psd.Sections;

namespace Razensoft.Psd.ValidationStrategies
{
    internal class HeaderSectionValidationStrategy : SectionValidationStrategy<HeaderSection>
    {
        private const string ExpectedSignature = "8BPS";

        private readonly int _expectedVersion;
        private readonly int _maxHeight;
        private readonly int _maxWidth;

        protected HeaderSectionValidationStrategy(int expectedVersion, int maxHeight, int maxWidth)
        {
            _expectedVersion = expectedVersion;
            _maxHeight = maxHeight;
            _maxWidth = maxWidth;
        }

        public override void Validate(HeaderSection section)
        {
            if (section.Signature != ExpectedSignature)
            {
                throw new InvalidPhotoshopFileException(
                    "File signature is invalid. " +
                    $"Expected: \"{ExpectedSignature}\", Actual: \"{section.Signature}\""
                );
            }

            if (section.Version != _expectedVersion)
            {
                throw new InvalidPhotoshopFileException(
                    "File version is invalid. " +
                    $"Expected: {_expectedVersion}, Actual: {section.Version}. " +
                    "Please use PhotoshopFile class for automatic file type resolution."
                );
            }

            if (section.ReservedBytes.Any(b => b != 0))
            {
                throw new InvalidPhotoshopFileException(
                    "Reserved bytes (7-12) are not null."
                );
            }

            if (section.ChannelCount < 1 || section.ChannelCount > 56)
            {
                throw new InvalidPhotoshopFileException(
                    "Image channel count is invalid. " +
                    $"Supported range: 1 to 56. Actual: {section.ChannelCount}"
                );
            }

            if (section.Height < 1 || section.Height > _maxHeight)
            {
                throw new InvalidPhotoshopFileException(
                    "Image height is invalid. " +
                    $"Supported range: 1 to {_maxHeight:N0}. Actual: {section.Height}"
                );
            }

            if (section.Width < 1 || section.Width > _maxWidth)
            {
                throw new InvalidPhotoshopFileException(
                    "Image width is invalid. " +
                    $"Supported range: 1 to {_maxWidth:N0}. Actual: {section.Width}"
                );
            }

            var supportedDepthValues = new[] { 1, 8, 16, 32 };
            if (!supportedDepthValues.Contains(section.Depth))
            {
                var values = supportedDepthValues.Select(v => v.ToString());
                throw new InvalidPhotoshopFileException(
                    "Image depth is invalid." +
                    $"Supported values: {string.Join(", ", values)}. Actual: {section.Depth}"
                );
            }

            var supportedColorModes = new[]
            {
                ColorMode.Bitmap,
                ColorMode.Grayscale,
                ColorMode.Indexed,
                ColorMode.Rgb,
                ColorMode.Cmyk,
                ColorMode.Multichannel,
                ColorMode.Duotone,
                ColorMode.Lab
            };
            if (!supportedColorModes.Contains(section.ColorMode))
            {
                var values = supportedColorModes.Cast<short>()
                    .Select(v => v.ToString());
                throw new InvalidPhotoshopFileException(
                    "File color mode is invalid." +
                    $"Supported values: {string.Join(", ", values)}, Actual: {section.Depth}"
                );
            }
        }
    }
}
