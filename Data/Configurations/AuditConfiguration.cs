using CoworkingReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoworkingReservationSystem.Data.Configurations
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.EntityId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Action)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(a => a.OldValues)
                .IsRequired(false);

            builder.Property(a => a.NewValues)
                .IsRequired(false);

            builder.Property(a => a.Timestamp)
                .IsRequired();

            // Relationships
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}