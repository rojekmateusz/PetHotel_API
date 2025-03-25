using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IImageRepository
{
    Task<int> CreateImage(Image entity);
}
