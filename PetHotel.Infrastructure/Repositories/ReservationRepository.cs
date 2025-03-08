using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class ReservationRepository(PetHotelDbContext dbContext) : IReservationRepository
{
    public async Task<int> CreateReservation(Reservation entity)
    {
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
