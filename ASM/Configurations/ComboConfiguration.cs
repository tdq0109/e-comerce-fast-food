using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class ComboConfiguration : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(c => c.ComboID);
            builder.Property(c => c.ComboName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.Price).HasColumnType("decimal(18,2)");
            builder.Property(c => c.ImageUrl).HasMaxLength(255);
        }
    }
}
