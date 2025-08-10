using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.User
{
    public class UserLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; }
    }
}
