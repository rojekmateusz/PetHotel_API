using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;
using PetHotel.Application.UseCases.Payment.Dto;
using PetHotel.Application.UseCases.Payment.Queries.GetAllPayments;
using PetHotel.Application.UseCases.Payment.Queries.GetPayment;

namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/{ownerId}/payments")]
public class PaymentController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> CreatePayment([FromRoute] int ownerId, CreatePaymentCommand command)
    { 
        command.OwnerId = ownerId;
        await mediator.Send(command);
        return Created(); 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAllPayments([FromRoute] int ownerId)
    {
        var payments = await mediator.Send(new GetAllPaymentsByOwnerIdQuery(ownerId));
        return Ok(payments);
    }

    [HttpGet("{paymentId}")]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayment([FromRoute] int ownerId, [FromRoute] int paymentId)
    {
        var payment = await mediator.Send(new GetPaymentByIdQuery(ownerId, paymentId));  
        return Ok(payment);
    }
}
