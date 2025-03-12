using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;
using PetHotel.Application.UseCases.Payment.Command.DeletePayment;
using PetHotel.Application.UseCases.Payment.Command.UpdatePayment;
using PetHotel.Application.UseCases.Payment.Dto;
using PetHotel.Application.UseCases.Payment.Queries.GetAllPayments;
using PetHotel.Application.UseCases.Payment.Queries.GetPayment;
using PetHotel.Domain.Constants;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/{ownerId}/payments")]
[Authorize]
public class PaymentController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> CreatePayment([FromRoute] int ownerId, CreatePaymentCommand command)
    {
        command.OwnerId = ownerId;
        var paymentId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetAllPayments), new { ownerId, paymentId }, null);
    }

    [HttpGet]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments([FromRoute] int ownerId)
    {
        var payments = await mediator.Send(new GetAllPaymentsByOwnerIdQuery(ownerId));
        return Ok(payments);
    }

    [HttpGet("{paymentId}")]
    [Authorize(Roles = UserRoles.User)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayment([FromRoute] int ownerId, [FromRoute] int paymentId)
    {
        var payment = await mediator.Send(new GetPaymentByIdQuery(ownerId, paymentId));
        return Ok(payment);
    }

    [HttpDelete("{paymentId}")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePayment([FromRoute] int ownerId, [FromRoute] int paymentId)
    {
        await mediator.Send(new DeletePaymentCommand(ownerId, paymentId));
        return NoContent();
    }

    [HttpPatch("{paymentId}")]
    [Authorize(Roles = UserRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePayment([FromRoute] int ownerId, [FromRoute] int paymentId, UpdatePaymentCommand command)
    {
        command.OwnerId = ownerId;
        command.Id = paymentId;
        
        await mediator.Send(command);
        return Ok();
    }
}
