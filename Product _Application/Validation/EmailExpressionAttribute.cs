using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Product__Application.Validation
{
    public class EmailExpressionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {

            string email = value.ToString();
            string pattern = @"^[\w.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            bool isEmailValid = Regex.IsMatch(email, pattern);
            if (isEmailValid)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalide Email");
        }
    }
}
