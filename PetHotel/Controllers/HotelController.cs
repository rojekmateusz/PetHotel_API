using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.Hotel.Command.CreateHotel;
using PetHotel.Application.Hotel.Command.DeleteHotel;
using PetHotel.Application.Hotel.Command.UpdateHotel;
using PetHotel.Application.Hotel.Queries.GetAllHotels;
using PetHotel.Application.Hotel.Queries.GetHotelById;

namespace PetHotel.API.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController(IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotel()
        {
            var hotels = await mediator.Send(new GetAllHotelsQuery());
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var hotel = await mediator.Send(new GetHotelByIdQuery(id));
            return Ok(hotel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel([FromRoute] int id)
        { 
            await mediator.Send(new DeleteHotelCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

    }
}
