using System;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Exceptions
{
    [Serializable]
    public class EntityDoesNotExistsException : Exception
    {
        public EntityDoesNotExistsException() { }
        public EntityDoesNotExistsException(string message) : base(message) { }
        public EntityDoesNotExistsException(string message, Exception inner) : base(message, inner) { }
        protected EntityDoesNotExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
