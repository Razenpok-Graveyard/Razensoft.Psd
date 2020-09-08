using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Razensoft.Psd.Tests
{
    public class HeaderSectionTests
    {
        [Test]
        [TestCase("files/header/channels-3.psd", 3)]
        [TestCase("files/header/channels-3.psb", 3)]
        public void Should_correctly_read_channel_count([NotNull] string path, int expectedChannelCount)
        {
            using var psdFile = PhotoshopFile.Open(path);
            psdFile.ChannelCount.Should()
                .Be(expectedChannelCount);
        }

        [Test]
        [TestCase("files/header/size-100x100.psd", 100, 100)]
        [TestCase("files/header/size-100x100.psb", 100, 100)]
        public void Should_correctly_read_image_size([NotNull] string path, int expectedHeight, int expectedWidth)
        {
            using var psdFile = PhotoshopFile.Open(path);
            psdFile.Height.Should()
                .Be(expectedHeight);
            psdFile.Width.Should()
                .Be(expectedWidth);
        }

        // TODO: All depth values
        [Test]
        [TestCase("files/header/depth-8.psd", 8)]
        [TestCase("files/header/depth-8.psb", 8)]
        public void Should_correctly_read_depth([NotNull] string path, int expectedDepth)
        {
            using var psdFile = PhotoshopFile.Open(path);
            psdFile.Depth.Should()
                .Be(expectedDepth);
        }

        // TODO: All color modes
        [Test]
        [TestCase("files/header/color-mode-rgb.psd", ColorMode.Rgb)]
        [TestCase("files/header/color-mode-rgb.psb", ColorMode.Rgb)]
        public void Should_correctly_read_color_mode([NotNull] string path, ColorMode expectedColorMode)
        {
            using var psdFile = PhotoshopFile.Open(path);
            psdFile.ColorMode.Should()
                .Be(expectedColorMode);
        }
    }
}
