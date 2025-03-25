using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class ImageRepository(PetHotelDbContext dbContext) : IImageRepository
{
    public async Task<int> CreateImage(Image entity)
    {
        dbContext.Images.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
