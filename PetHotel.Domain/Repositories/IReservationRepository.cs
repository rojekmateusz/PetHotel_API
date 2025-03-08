using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IReservationRepository
{
    Task<int> CreateReservation(Reservation entity);
}
