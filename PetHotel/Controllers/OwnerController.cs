using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Owner.Command.CreateOwner;
using PetHotel.Application.UseCases.Owner.Command.DeleteOwner;
using PetHotel.Application.UseCases.Owner.Command.UpdateOwner;
using PetHotel.Application.UseCases.Owner.Dto;
using PetHotel.Application.UseCases.Owner.Queries.GetAllOwners;
using PetHotel.Application.UseCases.Owner.Queries.GetOwnerById;
using PetHotel.Domain.Constants;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/owners")]
public class OwnerController(IMediator mediator): ControllerBase
{
    [HttpPost]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> CreateOwner([FromBody] CreateOwnerCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id}, null);
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAllOwners()
    {
        var owners = await mediator.Send(new GetAllOwnersQuery());
        return Ok(owners);
    }

    [HttpGet("{ownerId}")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> GetById([FromRoute] int ownerId)
    {
        var owner = await mediator.Send(new GetOwnerByIDQuery(ownerId));
        return Ok(owner);
    }

    [HttpDelete("{ownerId}")]
    [Authorize(Roles = UserRoles.User)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOwner([FromRoute] int ownerId)
    {
        await mediator.Send(new DeleteOwnerCommand(ownerId));
        return NoContent();
    }

    [HttpPatch("{ownerId}")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> UpdateOwner([FromRoute] int ownerId, [FromBody] UpdateOwnerCommand command)
    {
        command.Id = ownerId;
        await mediator.Send(command);
        return Ok();
    }
}
