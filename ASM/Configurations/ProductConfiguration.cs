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
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.ImageURL).HasMaxLength(255);
            builder.Property(p => p.Topic).HasMaxLength(100);
            builder.Property(p => p.Tags).HasMaxLength(255);
            builder.Property(p => p.IsAvailable).HasDefaultValue(true);
            builder.Property(p => p.IsCombo).HasDefaultValue(false);
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryID);
            builder.HasData(
            new Product
            {
                ProductID = 1,
                ProductName = "Burger Bò Phô Mai",
                Description = "Burger thịt bò, phô mai và rau",
                Price = 55000,
                Quantity = 100,
                ImageURL = "/images/burger.jpg",
                CategoryID = 1,
                IsAvailable = true,
                IsCombo = false,
                Topic = "Best Seller",
                Tags = "burger,bo,pho mai",
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductID = 2,
                ProductName = "Pizza Hải Sản",
                Description = "Pizza topping hải sản và phô mai",
                Price = 120000,
                Quantity = 100,
                ImageURL = "/images/pizza.jpg",
                CategoryID = 2,
                IsAvailable = true,
                IsCombo = false,
                Topic = "Mới",
                Tags = "pizza,hai san",
                CreatedAt = DateTime.Now
            },
            new Product
            {
                ProductID = 3,
                ProductName = "Coca-Cola Lon",
                Description = "Thức uống có gas",
                Price = 15000,
                Quantity = 100,
                ImageURL = "/images/coca.jpg",
                CategoryID = 3,
                IsAvailable = true,
                IsCombo = false,
                Topic = "Mới",
                Tags = "drink",
                CreatedAt = DateTime.Now
            }
        );
        }
    }
}
