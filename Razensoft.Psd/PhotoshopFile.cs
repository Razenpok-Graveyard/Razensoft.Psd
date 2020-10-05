using System;
using System.IO;
using JetBrains.Annotations;
using Razensoft.Psd.Debug;
using Razensoft.Psd.ReadingStrategies;
using Razensoft.Psd.Sections;
using Razensoft.Psd.Sections.ImageResources;
using Razensoft.Psd.Sections.LayerMaskInformation;
using Razensoft.Psd.ValidationStrategies;

namespace Razensoft.Psd
{
    [PublicAPI]
    public sealed class PhotoshopFile : IDisposable
    {
        private readonly Stream _stream;
        private readonly PhotoshopFileReader _reader;

        [NotNull] private readonly HeaderSection _headerSection;
        [NotNull] private readonly ColorModeDataSection _colorModeDataSection;
        [NotNull] private readonly ImageResourcesSection _imageResourcesSection;
        [NotNull] private readonly LayerMaskInformationSection _layerMaskInformationSection;
        [NotNull] private readonly ImageDataSection _imageDataSection;

        private PhotoshopFile(
            [NotNull] Stream stream,
            [NotNull] PhotoshopFileSectionReader<HeaderSection> headerSectionReader,
            [NotNull] PhotoshopFileSectionReader<ColorModeDataSection> colorModeDataSectionReader,
            [NotNull] PhotoshopFileSectionReader<ImageResourcesSection> imageResourcesSectionReader,
            [NotNull] PhotoshopFileSectionReader<LayerMaskInformationSection> layerMaskInformationSectionReader,
            [NotNull] PhotoshopFileSectionReader<ImageDataSection> imageDataSectionReader)
        {
            Assert.NotNull(stream, nameof(stream));
            Assert.NotNull(headerSectionReader, nameof(headerSectionReader));
            Assert.NotNull(colorModeDataSectionReader, nameof(colorModeDataSectionReader));
            Assert.NotNull(imageResourcesSectionReader, nameof(imageResourcesSectionReader));
            Assert.NotNull(layerMaskInformationSectionReader, nameof(layerMaskInformationSectionReader));
            Assert.NotNull(imageDataSectionReader, nameof(imageDataSectionReader));

            _stream = stream;
            _reader = new PhotoshopFileReader(stream);

            _headerSection = headerSectionReader.ReadSection(_reader, this);
            _colorModeDataSection = colorModeDataSectionReader.ReadSection(_reader, this);
            _imageResourcesSection = imageResourcesSectionReader.ReadSection(_reader, this);
            _layerMaskInformationSection = layerMaskInformationSectionReader.ReadSection(_reader, this);
            _imageDataSection = imageDataSectionReader.ReadSection(_reader, this);
        }

        public int Version => _headerSection.Version;

        public int ChannelCount => _headerSection.ChannelCount;

        public int Height => _headerSection.Height;

        public int Width => _headerSection.Width;

        public int Depth => _headerSection.Depth;

        public ColorMode ColorMode => _headerSection.ColorMode;

        public byte[] ColorModeData => _colorModeDataSection.ColorData;

        public byte[] ImageResourcesData => _imageResourcesSection.Data;

        public byte[] ImageData => _imageDataSection.Data;

        public void Save()
        {

        }

        public void SaveAs(string path)
        {
        }

        public void Dispose() => _stream?.Dispose();

        [NotNull]
        public static PhotoshopFile Open([NotNull] string path)
        {
            Assert.NotNull(path, nameof(path));
            var extension = Path.GetExtension(path);
            switch (extension)
            {
                case ".psd":
                    return OpenPsd(path);
                case ".psb":
                    return OpenPsb(path);
            }

            // TODO: Proper exception message
            throw new ArgumentException("Provided file path is invalid.");
        }

        [NotNull]
        public static PhotoshopFile OpenPsd([NotNull] string path)
        {
            Assert.NotNull(path, nameof(path));
            var fileStream = File.OpenRead(path);
            return new PhotoshopFile(
                fileStream,
                new PhotoshopFileSectionReader<HeaderSection>(
                    new HeaderSectionReadingStrategy(),
                    new PsdHeaderSectionValidationStrategy()
                ),
                new PhotoshopFileSectionReader<ColorModeDataSection>(
                    new ColorModeDataSectionReadingStrategy()
                ),
                new PhotoshopFileSectionReader<ImageResourcesSection>(
                    new ImageResourcesSectionReadingStrategy()
                ),
                new PhotoshopFileSectionReader<LayerMaskInformationSection>(
                    new PsdLayerMaskInformationSectionReadingStrategy()
                ),
                new PhotoshopFileSectionReader<ImageDataSection>(
                    new ImageDataSectionReadingStrategy()
                )
            );
        }

        [NotNull]
        public static PhotoshopFile OpenPsb([NotNull] string path)
        {
            Assert.NotNull(path, nameof(path));
            var fileStream = File.OpenRead(path);
            return new PhotoshopFile(
                fileStream,
                new PhotoshopFileSectionReader<HeaderSection>(
                    new HeaderSectionReadingStrategy(),
                    new PsbHeaderSectionValidationStrategy()
                ),
                new PhotoshopFileSectionReader<ColorModeDataSection>(
                    new ColorModeDataSectionReadingStrategy()
                ),
                new PhotoshopFileSectionReader<ImageResourcesSection>(
                    new ImageResourcesSectionReadingStrategy()
                ),
                new PhotoshopFileSectionReader<LayerMaskInformationSection>(
                    new PsbLayerMaskInformationSectionReadingStrategy()
                ),
                new PhotoshopFileSectionReader<ImageDataSection>(
                    new ImageDataSectionReadingStrategy()
                )
            );
        }
    }
}
