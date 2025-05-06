using CoworkingReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoworkingReservationSystem.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.StartTime)
                .IsRequired();

            builder.Property(b => b.EndTime)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(b => b.CancellationReason)
                .HasMaxLength(500)
                .IsRequired(false); // Change to not required

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.UpdatedAt)
                .IsRequired();

            // Relationships
            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}