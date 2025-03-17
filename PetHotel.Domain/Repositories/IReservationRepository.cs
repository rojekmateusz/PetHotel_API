using PetHotel.Domain.Entities;

namespace PetHotel.Domain.Repositories;

public interface IReservationRepository
{
    Task<int> CreateReservation(Reservation entity);
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task UpdateReservation(Reservation reservation);
}
