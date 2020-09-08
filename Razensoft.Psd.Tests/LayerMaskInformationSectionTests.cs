using JetBrains.Annotations;
using NUnit.Framework;

namespace Razensoft.Psd.Tests
{
    public class LayerMaskInformationSectionTests
    {
        [Test]
        [Ignore("TODO")]
        [TestCase("files/layer-mask-information/empty.psb")]
        [TestCase("files/layer-mask-information/empty.psd")]
        public void Should_correctly_read_layer_mask_information_section([NotNull] string path)
        {
            Assert.Fail();
        }
    }
}
