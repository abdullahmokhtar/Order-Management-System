using System.ComponentModel.DataAnnotations;

namespace RouteSummitTask.PL.Dtos
{
    public class CustomerRegisterDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password length must be between 8 to 16 characters")]
        [RegularExpression("^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$", ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase, 1 special character, 1 digit and length 8 characters")]
        public string Password { get; set; }
    }
}
