using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IAnimalRepository
{
    Task<int> CreateAnimal(Animal entity);
    Task<IEnumerable<Animal>> GetAllAnimalsAsync();
    Task<Animal?> GetAnimalByIdAsync(int id);
    Task DeleteAnimal(Animal entity);
    Task<int> UpdateAnimal(Animal entity);
    Task SaveChanges();
}
