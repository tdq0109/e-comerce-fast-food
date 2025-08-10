using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class ComboItemConfiguration : IEntityTypeConfiguration<ComboItem>
    {
        public void Configure(EntityTypeBuilder<ComboItem> builder)
        {
            builder.HasKey(ci => new { ci.ComboID, ci.ProductID });
            builder.HasOne(ci => ci.Combo)
                   .WithMany(c => c.ComboItems)
                   .HasForeignKey(ci => ci.ComboID);
            builder.HasOne(ci => ci.Product)
                   .WithMany(p => p.ComboItems)
                   .HasForeignKey(ci => ci.ProductID);
            builder.HasData(
           new ComboItem
           {
               ComboID = 1,
               ProductID = 1, // Burger
               Quantity = 1
           },
           new ComboItem
           {
               ComboID = 1,
               ProductID = 3, // Coca
               Quantity = 1
           }
       );
        }
    }
}
