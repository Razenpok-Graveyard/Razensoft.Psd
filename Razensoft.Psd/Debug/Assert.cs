using System.Diagnostics;
using JetBrains.Annotations;

namespace Razensoft.Psd.Debug
{
    internal static class Assert
    {
        [AssertionMethod]
        [Conditional("DEBUG")]
        [ContractAnnotation("value:null => halt")]
        public static void NotNull<T>([CanBeNull] T value, [NotNull] [InvokerParameterName] string paramName)
            => That(value != null, $"Value cannot be null. Parameter name: {paramName}");

        [AssertionMethod]
        [Conditional("DEBUG")]
        [ContractAnnotation("condition:false => halt")]
        public static void That(bool condition, [NotNull] string message)
        {
            if (!condition)
            {
                throw new AssertionException(message);
            }
        }
    }
}
