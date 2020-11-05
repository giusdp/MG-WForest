using System;

namespace PiBa.Exceptions
{
    public class GraphicsInfoNotInitializedException : Exception
    {
        public GraphicsInfoNotInitializedException()
        {
        }

        public GraphicsInfoNotInitializedException(string message): base(message)
        {
        }

        public GraphicsInfoNotInitializedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}