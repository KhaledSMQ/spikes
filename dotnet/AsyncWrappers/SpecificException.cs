using System;

namespace AsyncWrappers
{
    public class SpecificException : Exception
    {
        public SpecificException(string message)
            : base(message)
        { }
    }
}
