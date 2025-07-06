namespace ASM.Models
{
        public class User
        {
            public int UserID { get; set; }
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PasswordHash { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public string Role { get; set; } = "Customer";
            public string? AvatarUrl { get; set; }
            public string? GoogleId { get; set; }
            public bool IsActive { get; set; } = true;
            public string? Gender { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

            public ICollection<Order>? Orders { get; set; }
            public ICollection<Cart>? Carts { get; set; }
            public ICollection<Feedback>? Feedbacks { get; set; }
        }
}
