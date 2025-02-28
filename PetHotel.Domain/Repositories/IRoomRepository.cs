using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IRoomRepository
{
    Task<int> CreateRoom(Room entity);
    Task DeleteRoom(Room entity);
    Task SaveChanges();
}

