using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<int> CreateOwner(Owner entity);
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int id);
        Task<int> UpdateOwner(Owner entity);
        Task DeleteOwner(Owner entity);
        Task SaveChanges();
    }
}
