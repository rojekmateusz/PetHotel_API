using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using PetHotel.Application.UseCases.Room.Dto;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/{HotelId}/rooms")]
public class RoomController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<RoomDto>>> CreateRoom([FromRoute] int HotelId, CreateRoomCommand command)
    {
        command.HotelId = HotelId;
        await mediator.Send(command);
        return Created();
    }
}
