using System;

namespace WForest.Exceptions
{
    /// <summary>
    /// Exception for using the WForestFactory without initializing first.
    /// </summary>
    public class WForestNotInitializedException : Exception
    {

        /// <summary>
        /// Creates a new WForestNotInitializedException with a message.
        /// </summary>
        /// <param name="message"></param>
        public WForestNotInitializedException(string? message) : base(message)
        {
        }
    }
}