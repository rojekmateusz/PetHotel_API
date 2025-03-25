using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Animal.Command.DeleteAnimal;
using PetHotel.Application.UseCases.Animal.Queries.GetAllAnimals;
using PetHotel.Application.UseCases.Animal.Queries.GetAnimalById;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Animal.Command.UpdateAnimal;
using PetHotel.Application.UseCases.Animal.Dto;
using Microsoft.AspNetCore.Authorization;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Entities;

namespace PetHotel.API.Controllers
{
    [ApiController]
    [Route("api/{ownerId}/animals")]
    public class AnimalController(IMediator mediator): ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> CreateAnimal([FromRoute] int ownerId, [FromBody] CreateAnimalCommand command)
        {
            command.OwnerID = ownerId;
            int animalId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { ownerId, animalId }, null);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAllAnimal([FromRoute] int ownerId)
        {
            var animals = await mediator.Send(new GetAllAnimalsQuery(ownerId));
            return Ok(animals); 
        }

        [HttpGet("{animalId}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<IEnumerable<AnimalDto>>> GetById([FromRoute] int ownerId, [FromRoute] int animalId)
        {
            var animal = await mediator.Send(new GetAnimalByIdQuery(ownerId, animalId));
            return Ok(animal);
        }

        [HttpDelete("{animalId}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int ownerId, [FromRoute] int animalId)
        {
            await mediator.Send(new DeleteAnimalCommand(ownerId, animalId));
            return NoContent();
        }

        [HttpPatch("{animalId}")]
        [Authorize(Roles = UserRoles.User)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int ownerId, [FromRoute] int animalId, [FromBody] UpdateAnimalCommand command)
        {
            command.OwnerID = ownerId;
            command.Id = animalId;
            await mediator.Send(command);
            return Ok();
        }
    }
}
