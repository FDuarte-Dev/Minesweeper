using System;
using System.Runtime.Serialization;

namespace MineSweeper
{
    [Serializable]
    internal class AlreadyDugException : Exception
    {
        public AlreadyDugException()
        {
        }

        public AlreadyDugException(string message) : base(message)
        {
        }

        public AlreadyDugException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyDugException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}