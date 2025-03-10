using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IServiceRepository
{
    Task<int> CreateService(Service entity);
    
}
