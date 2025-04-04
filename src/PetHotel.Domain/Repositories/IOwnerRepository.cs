﻿using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<int> CreateOwner(Owner entity);
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner?> GetOwnerByIdAsync(int id);
        Task DeleteOwner(Owner entity);
        Task SaveChanges();
        Task<bool> DoesOwnerExist(string userId);
    }
}
