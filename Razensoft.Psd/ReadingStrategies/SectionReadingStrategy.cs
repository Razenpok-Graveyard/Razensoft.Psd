using JetBrains.Annotations;

namespace Razensoft.Psd.ReadingStrategies
{
    internal abstract class SectionReadingStrategy<T>
    {
        [NotNull]
        public abstract T Read([NotNull] PhotoshopFileReader reader, [NotNull] PhotoshopFile photoshopFile);
    }
}
