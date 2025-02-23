using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Entities;

namespace PetHotel.Infrastructure.Persistance
{
    internal class PetHotelDbContext(DbContextOptions<PetHotelDbContext> options) : DbContext(options)
    {
        internal DbSet<Hotel> Hotels { get; set; }
        internal DbSet<Room> Rooms { get; set; }
        internal DbSet<Animal> Animals { get; set; }
        internal DbSet<Owner> Owners { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Reservation> Reservations { get; set; }
        internal DbSet<Image> Images { get; set; }
        internal DbSet<Review> Reviews { get; set; }
        internal DbSet<Payment> Payments { get; set; }
        internal DbSet<ReservationService> ReservationServices { get; set; }
        internal DbSet<Role> Roles { get; set; }
        internal DbSet<Service> Services { get; set; }
        internal DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Reservations)
                .WithOne(r => r.Animal) 
                .HasForeignKey(a => a.AnimalId);

            modelBuilder.Entity<ReservationService>()
                .HasMany(a => a.Services)
                .WithOne()
                .HasForeignKey(a => a.ReservationServiceId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservationService)
                .WithOne(r => r.Reservation);

            modelBuilder.Entity<Hotel>()
                .HasMany(a => a.Reviews)
                .WithOne()
                .HasForeignKey(a => a.HotelId);

            modelBuilder.Entity<User>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId }); 

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

        }
    }
}
