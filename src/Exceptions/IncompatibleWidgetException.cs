using System;
using System.Runtime.Serialization;

namespace WForest.Exceptions
{
    public class IncompatibleWidgetException : Exception
    {
        public IncompatibleWidgetException()
        {
        }

        public IncompatibleWidgetException(string message) : base(message)
        {
        }

        public IncompatibleWidgetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}