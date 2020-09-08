using System;
using JetBrains.Annotations;

namespace Razensoft.Psd
{
    public class InvalidPhotoshopFileException : Exception
    {
        public InvalidPhotoshopFileException([NotNull] string message) : base(message) { }
    }
}
