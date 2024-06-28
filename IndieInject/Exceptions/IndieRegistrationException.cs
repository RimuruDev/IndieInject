using System;

namespace IndieInject
{
    public class IndieRegistrationException : Exception
    {
        public IndieRegistrationException() : base()
        {
        }

        public IndieRegistrationException(string message) : base(message)
        {
        }

        public IndieRegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}