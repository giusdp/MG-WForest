using System;
using System.Runtime.Serialization;

namespace WForest.Exceptions
{
    /// <summary>
    /// Exception for properties applied on incompatible widgets.
    /// </summary>
    public class IncompatibleWidgetException : Exception
    {
        /// <summary>
        /// Creates a new IncompatibleWidgetException with a message.
        /// </summary>
        /// <param name="message"></param>
        public IncompatibleWidgetException(string message) : base(message)
        {
        }
    }
}