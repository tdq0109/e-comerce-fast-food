using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class DeliveryRequestConfiguration : IEntityTypeConfiguration<DeliveryRequest>
    {
        public void Configure(EntityTypeBuilder<DeliveryRequest> builder)
        {
            builder.HasKey(dr => dr.RequestID);
            builder.Property(dr => dr.DeliveryNote).HasMaxLength(500);
            builder.HasOne(dr => dr.Order)
                   .WithOne(o => o.DeliveryRequest)
                   .HasForeignKey<DeliveryRequest>(dr => dr.OrderID);
        }
    }

}
