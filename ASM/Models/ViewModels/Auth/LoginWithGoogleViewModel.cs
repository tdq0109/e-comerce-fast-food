using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Auth
{
    public class LoginWithGoogleViewModel
    {
        [Required]
        public string IdToken { get; set; } = string.Empty;
    }
}
