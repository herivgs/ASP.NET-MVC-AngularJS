using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Coqueta.Types
{
    [Serializable]
    public class UserExistsExeption : BusinessLayerException
    {
        const string UserExistsExeptionMessage = "El usuario que intenta agregar actualmente se encuentra registrado existe";
        public UserExistsExeption() 
            : base(UserExistsExeptionMessage) { }

        public UserExistsExeption(string message) : base(message) { }
        public UserExistsExeption(string message, IEnumerable<string> errors) : base(message, errors)
        {
            Errors = errors;
        }
        public UserExistsExeption(Exception innerException, IEnumerable<string> errors) 
            : base(UserExistsExeptionMessage, innerException, errors) { }

        protected UserExistsExeption(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}