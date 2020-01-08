using System;
using System.Runtime.Serialization;

namespace MineSweeper
{
    [Serializable]
    internal class GameLostException : Exception
    {
        public GameLostException()
        {
        }

        public GameLostException(string message) : base(message)
        {
        }

        public GameLostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameLostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}