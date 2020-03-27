using System;

namespace Eafit.MarcosYPatrones.Cqrs.Application.Exceptions
{
    [Serializable]
    public class MultipleResultsForRequestedSingleEntityException : Exception
    {
        public MultipleResultsForRequestedSingleEntityException() { }
        public MultipleResultsForRequestedSingleEntityException(string message) : base(message) { }
        public MultipleResultsForRequestedSingleEntityException(string message, Exception inner) : base(message, inner) { }
        protected MultipleResultsForRequestedSingleEntityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
