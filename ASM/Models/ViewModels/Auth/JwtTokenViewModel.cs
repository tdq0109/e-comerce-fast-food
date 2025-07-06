namespace ASM.Models.ViewModels.Auth
{
    public class JwtTokenViewModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
