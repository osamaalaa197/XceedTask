using Product__Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace Product__Application.ViewModel
{
    public class RegisterViewModel
    {
        [MinLength(5)]
        [MaxLength(20)]
        public string? UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailExpression]
        public string? Email { get; set; }
    }
}
