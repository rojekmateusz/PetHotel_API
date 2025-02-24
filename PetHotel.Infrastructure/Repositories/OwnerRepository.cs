﻿using Microsoft.EntityFrameworkCore;
using PetHotel.Domain.Entities;
using PetHotel.Domain.Repositories;
using PetHotel.Infrastructure.Persistance;

namespace PetHotel.Infrastructure.Repositories;

internal class OwnerRepository(PetHotelDbContext dbContext) : IOwnerRepository
{
    public async Task<int> CreateOwner(Owner entity)
    {
        dbContext.Owners.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
    {
        var owners = await dbContext.Owners.ToListAsync();
        return owners;
    }

    public async Task<Owner?> GetOwnerByIdAsync(int id)
    {
        var owner = await dbContext.Owners
            .Include(a => a.Animals)
            .Include(p => p.Payments)
            .FirstOrDefaultAsync(o => o.Id == id);
        return owner;
    }
}
