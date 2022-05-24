using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
