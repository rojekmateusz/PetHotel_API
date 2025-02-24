using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class AnimalRepository(PetHotelDbContext dbContext) : IAnimalRepository
{
    public async Task<int> CreateAnimal(Animal entity)
    {
        dbContext.Animals.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
    {
        var animals = await dbContext.Animals.ToListAsync();
        return animals;
    }

    public async Task<Animal?> GetAnimalByIdAsync(int id)
    {
        var animal = await dbContext.Animals
            .Include(r => r.Reservations)
            .FirstOrDefaultAsync(r => r.Id == id);
        return animal;
    }
}
