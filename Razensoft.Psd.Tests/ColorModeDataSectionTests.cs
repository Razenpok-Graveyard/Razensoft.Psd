using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Razensoft.Psd.Tests
{
    public class ColorModeDataSectionTests
    {
        [Test]
        [Ignore("TODO")]
        public void Should_correctly_read_indexed_color_data_section()
        {
            Assert.Fail();
        }

        [Test]
        [Ignore("TODO")]
        public void Should_correctly_read_duotone_data_section()
        {
            Assert.Fail();
        }

        [Test]
        [TestCase("files/color-mode-data/empty.psb")]
        [TestCase("files/color-mode-data/empty.psd")]
        public void Should_correctly_read_empty_data_section([NotNull] string path)
        {
            using var photoshopFile = PhotoshopFile.Open(path);
            photoshopFile.ColorModeData.Should()
                .BeEmpty();
        }
    }
}
