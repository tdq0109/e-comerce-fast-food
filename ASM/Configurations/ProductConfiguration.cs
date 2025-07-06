using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID);
            builder.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.ImageURL).HasMaxLength(255);
            builder.Property(p => p.Topic).HasMaxLength(100);
            builder.Property(p => p.Tags).HasMaxLength(255);
            builder.Property(p => p.IsAvailable).HasDefaultValue(true);
            builder.Property(p => p.IsCombo).HasDefaultValue(false);
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryID);
        }
    }
}
