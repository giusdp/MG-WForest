using System;

namespace WForest.Exceptions
{
    /// <summary>
    /// Exception for passing invalid values as radius to rounded prop.
    /// </summary>
    public class InvalidRadiusException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InvalidRadiusException(string message): base(message)
        {
        }
    }
}