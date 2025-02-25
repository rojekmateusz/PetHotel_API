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

    public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
    {
        var hotels = await dbContext.Hotels.ToListAsync();
        return hotels;
    }

    public async Task<Hotel?> GetHotelByIdAsync(int id)
    {
        var hotel = await dbContext.Hotels
            .Include(r => r.Rooms)
            .Include(re => re.Reservations)
            .Include(rev => rev.Reviews)
            .Include(i => i.Images)
            .FirstOrDefaultAsync(r => r.Id == id);
        return hotel;
    }
}
