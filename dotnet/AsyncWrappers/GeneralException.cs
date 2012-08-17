using System;

namespace AsyncWrappers
{
    public class GeneralException : Exception
    {
        public GeneralException(string message)
            : base(message)
        { }
    }
}
