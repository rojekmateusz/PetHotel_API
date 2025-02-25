using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;
using PetHotel.Application.UseCases.Animal.Queries.GetAllAnimals;
using PetHotel.Application.UseCases.Animal.Queries.GetAnimalById;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;
using PetHotel.Application.UseCases.Animal.Dto;

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
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetById([FromRoute] int id)
        {
            var animal = await mediator.Send(new GetAnimalByIdQuery(id));
            return Ok(animal);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await mediator.Send(new DeleteAnimalCommand(id));
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAnimalCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
