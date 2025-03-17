using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IHotelRepository
{
    Task<int> CreateHotel(Hotel entity);
    Task<IEnumerable<Hotel>> GetAllHotelsAsync(); 
    Task<Hotel> GetHotelByIdAsync(int id);
    Task DeleteHotel(Hotel entity);
    Task SaveChanges();
    Task<(IEnumerable<Hotel>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}
