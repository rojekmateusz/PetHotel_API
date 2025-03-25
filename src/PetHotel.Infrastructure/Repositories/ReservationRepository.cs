using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;
using System.Threading;

namespace PetHotel.Infrastructure.Repositories;

internal class ReservationRepository(PetHotelDbContext dbContext) : IReservationRepository
{
    public async Task<int> CreateReservation(Reservation entity)
    {
        dbContext.Reservations.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.ReservationId;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
       var reservations = await dbContext.Reservations
           .Include(r => r.ReservationServices)
           .ThenInclude(rs => rs.Service)
           .ToListAsync();
       return reservations;
    }

    public async Task<Reservation?> GetReservationByIdAsync(int id)
    {
        var reservation = await dbContext.Reservations
            .Include(r => r.ReservationServices)
            .ThenInclude(rs => rs.Service)
            .FirstOrDefaultAsync(r => r.ReservationId == id);
                   
        return reservation; 
    }

    public async Task UpdateReservation(Reservation reservation)
    {
        dbContext.Reservations.Update(reservation);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteReservation(Reservation reservation)
    {
        dbContext.Reservations.Remove(reservation);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
