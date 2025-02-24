using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.Owner.Command.CreateOwner;
using PetHotel.Application.Owner.Queries.GetAllOwners;
using PetHotel.Application.Owner.Queries.GetOwnerById;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/owners")]
public class OwnerController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOwner([FromBody] CreateOwnerCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new {id}, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        var owners = await mediator.Send(new GetAllOwnersQuery());
        return Ok(owners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var owner = await mediator.Send(new GetOwnerByIDQuery(id));
        return Ok(owner);
    }
}
