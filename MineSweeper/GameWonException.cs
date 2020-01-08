using System;
using System.Runtime.Serialization;

namespace MineSweeper
{
    [Serializable]
    internal class GameWonException : Exception
    {
        public GameWonException()
        {
        }

        public GameWonException(string message) : base(message)
        {
        }

        public GameWonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameWonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}