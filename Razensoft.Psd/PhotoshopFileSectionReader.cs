using JetBrains.Annotations;
using Razensoft.Psd.Debug;
using Razensoft.Psd.ReadingStrategies;
using Razensoft.Psd.ValidationStrategies;

namespace Razensoft.Psd
{
    internal class PhotoshopFileSectionReader<T>
    {
        [NotNull] private readonly SectionReadingStrategy<T> _readingStrategy;
        [CanBeNull] private readonly SectionValidationStrategy<T> _validationStrategy;

        public PhotoshopFileSectionReader(
            [NotNull] SectionReadingStrategy<T> readingStrategy,
            [CanBeNull] SectionValidationStrategy<T> validationStrategy = null)
        {
            Assert.NotNull(readingStrategy, nameof(readingStrategy));
            _readingStrategy = readingStrategy;
            _validationStrategy = validationStrategy;
        }

        [NotNull]
        public T ReadSection([NotNull] PhotoshopFileReader reader, [NotNull] PhotoshopFile file)
        {
            Assert.NotNull(reader, nameof(reader));
            Assert.NotNull(file, nameof(file));
            var value = _readingStrategy.Read(reader, file);
            _validationStrategy?.Validate(value);
            return value;
        }
    }
}
