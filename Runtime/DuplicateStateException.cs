using System;
using System.Runtime.Serialization;

namespace UnityStateMachine
{
    [Serializable]
    internal class DuplicateStateException : Exception
    {
        public DuplicateStateException()
        {
        }

        public DuplicateStateException(string message) : base(message)
        {
        }

        public DuplicateStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}