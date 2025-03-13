using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Entities;

namespace PetHotel.Infrastructure.Persistance
{
    internal class PetHotelDbContext(DbContextOptions<PetHotelDbContext> options) : IdentityDbContext<User>(options)
    {
        internal DbSet<Hotel> Hotels { get; set; }
        internal DbSet<Room> Rooms { get; set; }
        internal DbSet<Animal> Animals { get; set; }
        internal DbSet<Owner> Owners { get; set; }
        internal DbSet<Reservation> Reservations { get; set; }
        internal DbSet<Image> Images { get; set; }
        internal DbSet<Review> Reviews { get; set; }
        internal DbSet<Payment> Payments { get; set; }
        internal DbSet<ReservationService> ReservationServices { get; set; }
        internal DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entities

            modelBuilder.Entity<Animal>()
               .HasMany(a => a.Reservations)
               .WithOne(r => r.Animal)
               .HasForeignKey(a => a.AnimalId);

            modelBuilder.Entity<Owner>()
                .HasMany(a => a.Animals)
                .WithOne(o => o.Owner)
                .HasForeignKey(a => a.OwnerID);

            modelBuilder.Entity<Owner>()
                .HasMany(a => a.Payments)
                .WithOne(p => p.Owner)
                .HasForeignKey(a => a.OwnerId);

            modelBuilder.Entity<Hotel>()
                .HasMany(a => a.Images)
                .WithOne()
                .HasForeignKey(a => a.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(a => a.Reservations)
                .WithOne(h => h.Hotel)
                .HasForeignKey(a => a.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne()
                .HasForeignKey(r => r.HotelId);

            modelBuilder.Entity<Hotel>()
                .HasMany(a => a.Reviews)
                .WithOne()
                .HasForeignKey(a => a.HotelId);

            modelBuilder.Entity<ReservationService>()
                .HasKey(rs => new { rs.ServiceId, rs.ReservationId });

            modelBuilder.Entity<ReservationService>()
                .HasOne(rs => rs.Service)
                .WithMany(s => s.ReservationServices)
                .HasForeignKey(rs => rs.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationService>()
                .HasOne(rs => rs.Reservation)
                .WithMany(s => s.ReservationServices)
                .HasForeignKey(rs => rs.ReservationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hotel>()
                .HasMany(s => s.Services)
                .WithOne()
                .HasForeignKey(s => s.HotelId);

            // User resource authorization

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedAnimals)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedHotels)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedImages)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasOne(o => o.OwnedOwner)
             .WithOne(r => r.user)
             .HasForeignKey<Owner>(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedPayments)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedReservations)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedReviews)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedRooms)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
             .HasMany(o => o.OwnedServices)
             .WithOne(r => r.user)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
