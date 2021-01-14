using System;

namespace WForest.Exceptions
{
    public class FontStoreNotInitializedException : Exception
    {
        public FontStoreNotInitializedException()
        {
        }

        public FontStoreNotInitializedException(string message) : base(message)
        {
        }

        public FontStoreNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}