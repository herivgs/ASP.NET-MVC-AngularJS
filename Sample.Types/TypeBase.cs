using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coqueta.Types
{
    public abstract class TypeBase
    {
        public virtual void Validate()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (!isValid)
            {
                throw new TypeValidationException(results.Select(r => r.ErrorMessage));
            }
        }
    }
}
