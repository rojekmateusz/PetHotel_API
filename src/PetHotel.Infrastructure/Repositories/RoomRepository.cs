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

    public async Task DeleteRoom(Room entity)
    {
        dbContext.Rooms.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
