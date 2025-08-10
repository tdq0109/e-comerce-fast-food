using ASM.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models.ViewModels.User
{
    public class UserProfileViewModel
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string? AvatarUrl { get; set; }

        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile? Avatar { get; set; }
    }
}
