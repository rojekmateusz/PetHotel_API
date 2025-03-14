using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Hotel.Command.DeleteHotel;
using PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;
using PetHotel.Application.UseCases.Hotel.Queries.GetHotelById;
using PetHotel.Application.UseCases.Hotel.Command.CreateHotel;
using PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;
using PetHotel.Application.UseCases.Hotel.Dto;
using Microsoft.AspNetCore.Authorization;
using PetHotel.Domain.Constants;

namespace PetHotel.API.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    
    public class HotelController(IMediator mediator): ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<IEnumerable<HotelDto>>> CreateHotel([FromBody] CreateHotelCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetAllHotel()
        {
            var hotels = await mediator.Send(new GetAllHotelsQuery());
            return Ok(hotels);
        }

        [HttpGet("{hotelId}")]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetById([FromRoute] int hotelId)
        {
            var hotel = await mediator.Send(new GetHotelByIdQuery(hotelId));
            return Ok(hotel);
        }

        [HttpDelete("{hotelId}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel([FromRoute] int hotelId)
        { 
            await mediator.Send(new DeleteAnimalCommand(hotelId));
            return NoContent();
        }

        [HttpPatch("{hotelId}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHotel([FromRoute] int hotelId, [FromBody] UpdateHotelCommand command)
        {
            command.Id = hotelId;
            await mediator.Send(command);
            return Ok();
        }

    }
}
