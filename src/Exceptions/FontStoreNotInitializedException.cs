using System;

namespace WForest.Exceptions
{
    /// <summary>
    /// Exception related to the FontStore when it's used without setting a DefaultFont first.
    /// </summary>
    public class FontStoreNotInitializedException : Exception
    {
        
        /// <summary>
        /// Creates a new FontStoreNotInitializedException with a message.
        /// </summary>
        /// <param name="message"></param>
        public FontStoreNotInitializedException(string message) : base(message)
        {
        }
    }
}