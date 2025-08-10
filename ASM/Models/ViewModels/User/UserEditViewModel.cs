using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.User
{
    public class UserEditViewModel
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

        public string? Gender { get; set; }
        public string? AvatarUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public UserRole Role { get; set; } = UserRole.Customer;

        [NotMapped]
        public IFormFile? Avatar { get; set; }
    }
}
