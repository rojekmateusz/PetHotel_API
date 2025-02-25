using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IHotelRepository
{
    Task<int> CreateHotel(Hotel entity);
    Task<IEnumerable<Hotel>> GetAllHotelsAsync(); 
    Task<Hotel?> GetHotelByIdAsync(int id);
}
