using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using PetHotel.Application.UseCases.Room.Dto;
using PetHotel.Application.UseCases.Room.Queries.GetAllRoomsByHotelId;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRoomsByHotelId([FromRoute] int HotelId)
    {
        var rooms = await mediator.Send(new GetAllRoomsByHotelIdQuery(HotelId));
        return Ok(rooms);
    }
}
