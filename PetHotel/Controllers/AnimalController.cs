using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.Animal.Command.CreateAnimal;
using PetHotel.Application.Animal.Dto;
using PetHotel.Application.Animal.Queries.GetAllAnimal;
using PetHotel.Application.Animal.Queries.GetAnimalById;

namespace PetHotel.API.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController(IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> CreateAnimal([FromBody] CreateAnimalCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new {id}, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAllAnimal()
        {
            var animals = await mediator.Send(new GetAllAnimalsQuery());
            return Ok(animals); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var animal = await mediator.Send(new GetAnimalByIdQuery(id));
            return Ok(animal);
        }
    }
}
