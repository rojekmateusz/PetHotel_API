using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Service.Command.CreateService;
using PetHotel.Application.UseCases.Service.Command.DeleteService;
using PetHotel.Application.UseCases.Service.Command.UpdateService;
using PetHotel.Application.UseCases.Service.Dto;
using PetHotel.Application.UseCases.Service.Queries.GetAllServices;
using PetHotel.Application.UseCases.Service.Queries.GetServiceByID;
using PetHotel.Domain.Constants;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("{hotelId}/services")]
[Authorize(Roles = UserRoles.User)]
public class ServiceController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> CreateService([FromRoute] int hotelId, CreateServiceCommand command)
    {
        command.HotelId = hotelId;
        int serviceId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetServiceById), new { hotelId, serviceId }, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServices([FromRoute] int hotelId)
    {
        var services = await mediator.Send(new GetAllServicesQuery(hotelId));
        return Ok(services);
    }

    [HttpGet]
    [Route("{serviceId}")]
    public async Task<ActionResult<ServiceDto>> GetServiceById([FromRoute] int hotelId, [FromRoute] int serviceId)
    {
        var service = await mediator.Send(new GetServiceByIdQuery(hotelId, serviceId));
        return Ok(service);
    }

    [HttpDelete]
    [Route("{serviceId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> DeleteService([FromRoute] int hotelId, [FromRoute] int serviceId)
    {
        await mediator.Send(new DeleteServiceCommand(hotelId, serviceId));
        return NoContent();
    }

    [HttpPost]
    [Route("{serviceId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = UserRoles.User)]
    public async Task<IActionResult> UpdateService([FromRoute] int hotelId, [FromRoute] int serviceId, [FromBody] UpdateServiceCommand command)
    {
        command.HotelId = hotelId;
        command.ServiceId = serviceId;
        await mediator.Send(command);
        return Ok();
    }
}
