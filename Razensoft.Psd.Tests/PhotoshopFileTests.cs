using System;
using System.IO;
using FluentAssertions;
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
            using var photoshopFile = PhotoshopFile.Open(path);
        }

        [Test]
        [TestCase("files/invalid/text.txt")]
        public void Should_fail_to_open_non_photoshop_file(string path)
        {
            Assert.Throws<InvalidPhotoshopFileException>(
                () =>
                {
                    using var photoshopFile = PhotoshopFile.OpenPsd(path);
                }
            );
            Assert.Throws<InvalidPhotoshopFileException>(
                () =>
                {
                    using var photoshopFile = PhotoshopFile.OpenPsb(path);
                }
            );
        }

        [Test]
        [TestCase("files/empty.psb")]
        [TestCase("files/empty.psd")]
        public void Should_save_file_correctly([NotNull] string path)
        {
            using var photoshopFile = PhotoshopFile.Open(path);
            var tempPath = Path.GetTempPath();
            var guid = Guid.NewGuid();
            var tempFilePath = Path.Combine(tempPath, guid.ToString());
            photoshopFile.SaveAs(tempFilePath);
            File.Exists(tempFilePath)
                .Should()
                .Be(true);
            var expectedBytes = File.ReadAllBytes(path);
            var actualBytes = File.ReadAllBytes(tempFilePath);
            actualBytes.Should()
                .Equal(expectedBytes);
            // TODO: move cleanup out of here
            File.Delete(tempFilePath);
        }
    }
}
