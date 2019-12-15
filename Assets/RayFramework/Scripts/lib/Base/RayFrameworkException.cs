using System;
using System.Runtime.Serialization;

namespace RayFramework
{
    [Serializable]
    public class RayFrameworkException : Exception
    {
        public RayFrameworkException() : base() { }

        public RayFrameworkException(string message) : base(message) { }

        public RayFrameworkException(string message, Exception innerException)
            : base(message, innerException) { }

        protected RayFrameworkException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
