using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Owner.Command.CreateOwner;
using PetHotel.Application.UseCases.Owner.Command.DeleteOwner;
using PetHotel.Application.UseCases.Owner.Command.UpdateOwner;
using PetHotel.Application.UseCases.Owner.Dto;
using PetHotel.Application.UseCases.Owner.Queries.GetAllOwners;
using PetHotel.Application.UseCases.Owner.Queries.GetOwnerById;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/owners")]

public class OwnerController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> CreateOwner([FromBody] CreateOwnerCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id}, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAllOwners()
    {
        var owners = await mediator.Send(new GetAllOwnersQuery());
        return Ok(owners);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetById([FromRoute] int id)
    {
        var owner = await mediator.Send(new GetOwnerByIDQuery(id));
        return Ok(owner);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOwner([FromRoute] int id)
    {
        await mediator.Send(new DeleteOwnerCommand(id));
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOwner([FromRoute] int id, [FromBody] UpdateOwnerCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return Ok();
    }
}
