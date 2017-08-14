using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Coqueta.Types
{
    [Serializable]
    internal class TypeValidationException : BusinessLayerException
    {
        private const string TypeValidationExceptionMessage  = "Several errors have occured while validating";

        public TypeValidationException()
            :base (TypeValidationExceptionMessage)
        {
            Errors = new[] { TypeValidationExceptionMessage };
        }
        
        public TypeValidationException(IEnumerable<string> errors)
            : base(TypeValidationExceptionMessage, errors) { }

        public TypeValidationException(Exception innerException, IEnumerable<string> errors) 
            : base(TypeValidationExceptionMessage, innerException, errors) { }

        protected TypeValidationException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}