using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Phone).HasMaxLength(15);
            builder.Property(u => u.Address).HasMaxLength(200);
            builder.Property(u => u.Role).HasMaxLength(50).HasConversion<string>();
            builder.Property(u => u.AvatarUrl).HasMaxLength(255);
            builder.Property(u => u.Gender).HasMaxLength(10);
            builder.Property(u => u.GoogleId).HasMaxLength(100);
            builder.Property(u => u.IsActive).HasDefaultValue(true);
            builder.HasData(
          new User
          {
              UserID = -1, // dùng giá trị âm
              FullName = "Admin",
              Email = "admin@example.com",
              PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
              Phone = "0909999999",
              Address = "123 Main St",
              Role = UserRole.Admin,
              IsActive = true,
              CreatedAt = DateTime.Now
          },
    new User
    {
        UserID = -2,
        FullName = "Nguyễn Văn A",
        Email = "a@gmail.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
        Phone = "0912345678",
        Address = "456 Second St",
        Role = UserRole.Customer,
        IsActive = true,
        CreatedAt = DateTime.Now
    }
       );
        }
    }
}
