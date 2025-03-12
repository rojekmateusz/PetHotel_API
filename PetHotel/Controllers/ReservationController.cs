using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Reservatiion.CreateReservation;
using PetHotel.Application.UseCases.Reservatiion.Dto;
using PetHotel.Application.UseCases.Reservatiion.Queries.GetAllReservations;
using PetHotel.Application.UseCases.Reservatiion.Queries.GetReservationById;
using PetHotel.Domain.Constants;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/hotels/{hotelId}/reservations")]
[Authorize]

public class ReservationController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> CreateReservation([FromRoute] int hotelId, CreateReservationCommand command)
    {
        command.HotelId = hotelId;
        int reservationId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetReservationById), new { hotelId, reservationId }, null);
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations([FromRoute] int hotelId)
    {
        var reservations = await mediator.Send(new GetAllReservationsQuery(hotelId));
        return Ok(reservations);
    }

    [HttpGet]
    [Route("{reservationId}")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationById([FromRoute] int hotelId, [FromRoute] int reservationId)
    {
    var reservation = await mediator.Send(new GetReservationByIdQuery(hotelId, reservationId));
    return Ok(reservation);
    }
}
