using ASM.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASM.Configurations
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(f => f.FeedbackID);
            builder.Property(f => f.Comment).HasMaxLength(500);
            builder.Property(f => f.Type).HasMaxLength(50);
            builder.Property(f => f.Rating).IsRequired();
            builder.HasOne(f => f.User)
                   .WithMany(u => u.Feedbacks)
                   .HasForeignKey(f => f.UserID);
            builder.HasOne(f => f.Product)
                   .WithMany(p => p.Feedbacks)
                   .HasForeignKey(f => f.ProductID);
        }
    }

}
