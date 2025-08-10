using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.CartID);
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Carts)
                   .HasForeignKey(c => c.UserID)
                  .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Product)
                   .WithMany(p => p.Carts)
                   .HasForeignKey(c => c.ProductID)
                   .OnDelete(DeleteBehavior.Restrict) 
                   .IsRequired(false);
            builder.HasOne(c => c.Combo)
                   .WithMany(co => co.Carts)
                   .HasForeignKey(c => c.ComboID)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
        }
    }

}
