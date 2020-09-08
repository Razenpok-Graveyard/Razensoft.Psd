using JetBrains.Annotations;
using NUnit.Framework;

namespace Razensoft.Psd.Tests
{
    public class ImageResourcesSectionTests
    {
        [Test]
        [Ignore("TODO")]
        [TestCase("files/image-resources/empty.psb")]
        [TestCase("files/image-resources/empty.psd")]
        public void Should_correctly_read_image_resources_section([NotNull] string path)
        {
            Assert.Fail();
        }
    }
}
