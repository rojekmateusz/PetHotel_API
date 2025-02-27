﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using PetHotel.Application.UseCases.Room.Command.DeleteRoom;
using PetHotel.Application.UseCases.Room.Command.UpdateRoom;
using PetHotel.Application.UseCases.Room.Dto;
using PetHotel.Application.UseCases.Room.Queries.GetAllRoomsByHotelId;
using PetHotel.Application.UseCases.Room.Queries.GetRoomById;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/{HotelId}/rooms")]
public class RoomController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<RoomDto>>> CreateRoom([FromRoute] int HotelId, CreateRoomCommand command)
    {
        command.HotelId = HotelId;
        //int id = 
        await mediator.Send(command);
        //return CreatedAtAction(nameof(GetRoomById), new { id }, null ); cos tu jest nie tak z kodem
        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRoomsByHotelId([FromRoute] int HotelId)
    {
        var rooms = await mediator.Send(new GetAllRoomsByHotelIdQuery(HotelId));
        return Ok(rooms);
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomById([FromRoute] int HotelId, [FromRoute] int roomId)
    {
        var room = await mediator.Send(new GetRoomByIdQuery(HotelId, roomId));
        return Ok(room);
    }

    [HttpDelete("{roomId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRoom([FromRoute] int roomId, [FromRoute] int HotelId)
    {
        await mediator.Send(new DeleteRoomCommand(roomId, HotelId));
        return NoContent();
    }

    [HttpPatch("{roomId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRoom([FromRoute] int HotelId, [FromRoute] int roomId, UpdateRoomCommand command)
    {
        command.Id = roomId;
        command.HotelId = HotelId;
        await mediator.Send(command);
        return Ok();
    }
}
