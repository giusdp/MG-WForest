using System;

namespace PiBa.Exceptions
{
    public class WindowNotInitializedException : Exception
    {
        public WindowNotInitializedException()
        {
        }

        public WindowNotInitializedException(string message): base(message)
        {
        }

        public WindowNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}