using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class PaymentRepository(PetHotelDbContext dbContext) : IPaymentRepository
{
    public async Task<int> CreatePayment(Payment entity)
    {
        dbContext.Payments.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeletePayment(Payment entity)
    {
        dbContext.Payments.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
