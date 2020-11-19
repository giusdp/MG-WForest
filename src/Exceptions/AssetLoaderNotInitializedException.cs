using System;

namespace WForest.Exceptions
{
    public class AssetLoaderNotInitializedException : Exception
    {
        public AssetLoaderNotInitializedException()
        {
        }

        public AssetLoaderNotInitializedException(string message): base(message)
        {
        }

        public AssetLoaderNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}