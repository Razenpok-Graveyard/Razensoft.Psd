using JetBrains.Annotations;

namespace Razensoft.Psd.ValidationStrategies
{
    internal abstract class SectionValidationStrategy<T>
    {
        [AssertionMethod]
        public abstract void Validate([NotNull] T section);
    }
}
