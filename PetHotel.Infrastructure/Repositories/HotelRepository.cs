using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;
using System.Linq.Expressions;

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

    public async Task<(IEnumerable<Hotel>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
    {
        var serachPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext.Hotels
            .Where(h => serachPhraseLower == null
            || h.HotelName.ToLower().Contains(serachPhraseLower)
            || h.City.ToLower().Contains(serachPhraseLower));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Hotel, object>>>
             {
                 { nameof(Hotel.HotelName), r => r.HotelName },
                 { nameof(Hotel.Description), r => r.Description },
                 { nameof(Hotel.City), r => r.City },
             };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
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
