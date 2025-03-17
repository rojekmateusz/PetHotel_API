using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class HotelRepository(PetHotelDbContext dbContext) : IHotelRepository
{
    public async Task<int> CreateHotel(Hotel entity)
    {
        dbContext.Hotels.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteHotel(Hotel entity)
    {
        dbContext.Hotels.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
    {
        var hotels = await dbContext.Hotels.ToListAsync();
        return hotels;
    }

    public async Task<(IEnumerable<Hotel>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber)
    {
        var serachPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Hotels
            .Where(h => serachPhraseLower == null
            || h.HotelName.ToLower().Contains(serachPhraseLower)
            || h.City.ToLower().Contains(serachPhraseLower));

        var totalCount = await baseQuery.CountAsync();

        var hotels = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (hotels, totalCount);
    }

    public async Task<Hotel?> GetHotelByIdAsync(int id) 
    {
        var hotel = await dbContext.Hotels
            .Include(r => r.Rooms)
            .Include(re => re.Reservations)
            .Include(rev => rev.Reviews)
            .Include(i => i.Images)
            .Include(i => i.Services)
            .FirstOrDefaultAsync(r => r.Id == id);
        return hotel;
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
