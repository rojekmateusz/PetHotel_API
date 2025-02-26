using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class RoomRepository(PetHotelDbContext dbContext) : IRoomRepository
{
    public async Task<int> CreateRoom(Room entity)
    {
        dbContext.Rooms.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
