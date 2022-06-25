using System.ComponentModel.DataAnnotations;

namespace Guardian.Service.Request
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
