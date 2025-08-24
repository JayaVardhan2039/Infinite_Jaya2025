using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Custom_Validations_Prj.CustomValidations
{
    public class DOBValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null || !(value is DateTime dob))
            {
                return new ValidationResult("Date of Birth is required.");
            }

            var age = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-age)) age--;

            if (age < 21 || age > 25)
            {
                return new ValidationResult("Candidate must be between 21 and 25 years old.");
            }

            return ValidationResult.Success;

        }
    }
}