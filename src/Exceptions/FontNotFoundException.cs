using System;

namespace WForest.Exceptions
{
    /// <summary>
    /// Exception related to missing Fonts in the FontStore.
    /// </summary>
    public class FontNotFoundException : Exception
    {
        /// <summary>
        /// Creates a new FontNotFoundException with a message.
        /// </summary>
        /// <param name="message"></param>
        public FontNotFoundException(string message) : base(message)
        {
        }
    }
}