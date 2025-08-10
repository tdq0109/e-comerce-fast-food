using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderID);
            builder.Property(o => o.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Pending");
            builder.Property(o => o.DeliveryAddress).IsRequired().HasMaxLength(200);
            builder.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(o => o.CancelReason).HasMaxLength(255);
            builder.Property(o => o.Note).HasMaxLength(255);
            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserID);
        }
    }

}
