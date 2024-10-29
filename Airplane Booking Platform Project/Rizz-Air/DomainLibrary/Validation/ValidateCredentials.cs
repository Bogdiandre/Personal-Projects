using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Validation
{
    public class ValidateCredentials
    {

        public ValidateCredentials()
        {

        }
        public bool Validate(object obj, out string validationErrorMessage)
        {
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var validationContext = new ValidationContext(obj);

            if (Validator.TryValidateObject(obj, validationContext, validationResults, true))
            {
                validationErrorMessage = null;
                return true;
            }
            else
            {
                validationErrorMessage = string.Join("\n", validationResults.Select(result => result.ErrorMessage));
                return false;
            }
        }




    }
}
