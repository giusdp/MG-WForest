using System;

namespace PiBa.Exceptions
{
    public class WidgetNotRegisteredException : Exception
    {
        public WidgetNotRegisteredException()
        {
        }

        public WidgetNotRegisteredException(string message): base(message)
        {
        }

        public WidgetNotRegisteredException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}