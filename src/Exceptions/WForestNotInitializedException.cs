using System;

namespace WForest.Exceptions
{
    public class WForestNotInitializedException : Exception
    {
        public WForestNotInitializedException()
        {
        }

        public WForestNotInitializedException(string? message) : base(message)
        {
        }

        public WForestNotInitializedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}