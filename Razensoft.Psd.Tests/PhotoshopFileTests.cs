using JetBrains.Annotations;
using NUnit.Framework;

namespace Razensoft.Psd.Tests
{
    public class PhotoshopFileTests
    {
        [Test]
        [TestCase("files/empty.psb")]
        [TestCase("files/empty.psd")]
        public void Should_open_valid_file([NotNull] string path)
        {
            using var psbFile = PhotoshopFile.Open(path);
        }

        [Test]
        [TestCase("files/invalid/text.txt")]
        public void Should_fail_to_open_non_photoshop_file(string path)
        {
            Assert.Throws<InvalidPhotoshopFileException>(
                () =>
                {
                    using var psbFile = PhotoshopFile.OpenPsd(path);
                }
            );
            Assert.Throws<InvalidPhotoshopFileException>(
                () =>
                {
                    using var psbFile = PhotoshopFile.OpenPsb(path);
                }
            );
        }
    }
}
