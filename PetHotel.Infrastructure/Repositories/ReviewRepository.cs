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

    public async Task DeleteReview(Review entity)
    {
        dbContext.Reviews.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateReview(Review entity)
    {
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }
}
