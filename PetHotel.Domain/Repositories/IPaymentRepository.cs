using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IPaymentRepository
{
    Task<int> CreatePayment(Payment entity);
}
