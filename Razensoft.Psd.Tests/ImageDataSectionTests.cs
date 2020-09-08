using JetBrains.Annotations;
using NUnit.Framework;

namespace Razensoft.Psd.Tests
{
    public class ImageDataSectionTests
    {
        [Test]
        [Ignore("TODO")]
        [TestCase("files/image-data/empty.psb")]
        [TestCase("files/image-data/empty.psd")]
        public void Should_correctly_read_image_data_section([NotNull] string path)
        {
            Assert.Fail();
        }
    }
}
