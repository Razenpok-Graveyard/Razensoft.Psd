using System.IO;
using FluentAssertions;
using JetBrains.Annotations;
using NUnit.Framework;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Razensoft.Psd.Tests
{
    public class ImageDataSectionTests
    {
        [Test]
        [TestCase("files/image-data/empty.psb")]
        [TestCase("files/image-data/empty.psd")]
        public void Should_correctly_read_image_data_section([NotNull] string path)
        {
            const string exportedPngPath = "files/image-data/empty.png";
            using var photoshopFile = PhotoshopFile.Open(path);
            var image = Image.Load<Rgb24>(exportedPngPath);
            var channelStride = photoshopFile.Width * photoshopFile.Height;
            for (var i = 0; i < image.Height; i++)
            {
                var row = image.GetPixelRowSpan(i);
                for (var j = 0; j < image.Width; j++)
                {
                    var expectedPixel = row[j];
                    var actualR = photoshopFile.ImageData[i * image.Width + j];
                    var actualG = photoshopFile.ImageData[channelStride + i * image.Width + j];
                    var actualB = photoshopFile.ImageData[channelStride * 2 + i * image.Width + j];
                    actualR.Should()
                        .Be(expectedPixel.R);
                    actualG.Should()
                        .Be(expectedPixel.G);
                    actualB.Should()
                        .Be(expectedPixel.B);
                }
            }
        }
    }
}
