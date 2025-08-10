using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.OrderDetailID);
            builder.Property(od => od.UnitPrice).HasColumnType("decimal(18,2)");
            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderID);
            builder.HasOne(od => od.Product)
                   .WithMany(p => p.OrderDetails)
                   .HasForeignKey(od => od.ProductID)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(od => od.Combo)
                   .WithMany(p => p.OrderDetails)
                   .HasForeignKey(od => od.ComboID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
