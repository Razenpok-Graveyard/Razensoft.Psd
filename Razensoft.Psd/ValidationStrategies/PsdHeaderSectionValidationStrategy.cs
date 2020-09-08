namespace Razensoft.Psd.ValidationStrategies
{
    internal class PsdHeaderSectionValidationStrategy : HeaderSectionValidationStrategy
    {
        public PsdHeaderSectionValidationStrategy() : base(1, 30000, 30000) { }
    }
}
