using MediatR;

namespace PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;

public class UpdateAnimalCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Spiecies { get; set; } = default!;
    public string Breed { get; set; } = default!;
    public int? Age { get; set; }
    public decimal? Weight { get; set; }
    public string? Note { get; set; }
    public int OwnerID { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
