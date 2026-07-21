using ConferenceHallBooking.Domain.Entitities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceHallBooking.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Hall> Halls { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Amenities> Amenities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hall>(entity =>
            {
                entity.HasKey(h => h.Id);

                entity.Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(h => h.Capacity)
                .IsRequired();

                entity.Property(h => h.PricePerHour)
                .IsRequired();

                entity.HasMany(h => h.Bookings);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.HallId)
                .IsRequired();

                entity.Property(b => b.StartTime)
                .IsRequired();

                entity.Property(b => b.EndTime)
                .IsRequired();

                entity.Property(b => b.TotalPrice)
                .IsRequired();

                entity.HasOne(b => b.Hall)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HallId);
            });

            modelBuilder.Entity<Amenities>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(256);

                entity.Property(a => a.Price)
                .IsRequired();
            });
        }
    }
}
