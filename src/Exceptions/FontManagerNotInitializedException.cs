using System;

namespace WForest.Exceptions
{
    public class FontManagerNotInitializedException : Exception
    {
        public FontManagerNotInitializedException()
        {
        }

        public FontManagerNotInitializedException(string message) : base(message)
        {
        }

        public FontManagerNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}