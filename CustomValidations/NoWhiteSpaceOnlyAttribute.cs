using System.ComponentModel.DataAnnotations;

namespace StudentsUnite_II.CustomValidations
{
    public class NoWhiteSpaceOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           if(value !=  null && value is string str && string.IsNullOrWhiteSpace(str)) {
                return new ValidationResult("The field cannot contain only whitespace.");
           }

           return ValidationResult.Success;
        }
    }
}
