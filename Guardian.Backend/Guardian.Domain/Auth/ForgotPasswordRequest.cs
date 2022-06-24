using System.ComponentModel.DataAnnotations;

namespace Guardian.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
