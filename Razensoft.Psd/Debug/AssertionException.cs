using System;

namespace Razensoft.Psd.Debug
{
    internal class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) { }
    }
}
