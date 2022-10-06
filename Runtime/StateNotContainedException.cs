using System;
using System.Runtime.Serialization;

namespace StM
{
    [Serializable]
    internal class StateNotContainedException : Exception
    {
        public StateNotContainedException()
        {
        }

        public StateNotContainedException(string message) : base(message)
        {
        }

        public StateNotContainedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StateNotContainedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}