using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Coqueta.Types
{
    public class BusinessLayerException : Exception
    {
        public IEnumerable<string> Errors { get; protected set; }

        public BusinessLayerException() { }

        public BusinessLayerException(string message)
            : base(message) { }

        public BusinessLayerException(string message, IEnumerable<string> errors)
            : base(message)
        {
            Errors = errors;
        }

        public BusinessLayerException(string message, Exception inner, IEnumerable<string> errors)
            : base(message, inner)
        {
            Errors = errors;
        }
        protected BusinessLayerException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
    }
}
