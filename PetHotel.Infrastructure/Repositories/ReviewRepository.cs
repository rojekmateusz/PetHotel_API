using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class ReviewRepository(PetHotelDbContext dbContext) : IReviewRepository
{
    public async Task<int> CreateReview(Review entity)
    {
        dbContext.Reviews.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
