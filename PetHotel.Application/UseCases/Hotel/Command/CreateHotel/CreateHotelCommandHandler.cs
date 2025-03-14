using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PetHotel.Application.UseCases.Owner.Dto;
using PetHotel.Application.User;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Hotel.Command.CreateHotel;

public class CreateHotelCommandHandler(ILogger<CreateHotelCommandHandler> logger, IMapper mapper, IHotelRepository hotelRepository,
    IHotelAuthorizationService hotelAuthorizationService, IUserContext userContext) : IRequestHandler<CreateHotelCommand, int>
{
    public async Task<int> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create Hotel {@Hotel}", request);
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("{UserEmail} [{UserId}] is creating a new owner {@Hotel}", currentUser.Email, currentUser.Id, request);

        var hotel = mapper.Map<Domain.Entities.Hotel>(request);
        hotel.UserId = currentUser.Id;
        int id = await hotelRepository.CreateHotel(hotel);
        return id;
    }
}
