using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryID);
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);
            builder.HasData(
               new Category { CategoryID = 1, CategoryName = "Burger" },
               new Category { CategoryID = 2, CategoryName = "Pizza" },
               new Category { CategoryID = 3, CategoryName = "Nước uống" }
       );
        }
    }
}
