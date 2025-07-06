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
            builder.Property(u => u.Role).HasMaxLength(50).HasDefaultValue("Customer");
            builder.Property(u => u.AvatarUrl).HasMaxLength(255);
            builder.Property(u => u.Gender).HasMaxLength(10);
            builder.Property(u => u.GoogleId).HasMaxLength(100);
            builder.Property(u => u.IsActive).HasDefaultValue(true);
        }
    }
}
