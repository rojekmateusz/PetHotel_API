using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Domain.Entities;
using PetHotel.Application.UseCases.Hotel.Queries.GetHotelById;
using PetHotel.Application.UseCases.Review.Command.CreateReview;
using PetHotel.Application.UseCases.Review.Queries.GetReviewsByHotelId;
namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/hotels/{HotelId}/reviews")]
public class ReviewController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<Review>>> CreateReview([FromRoute] int HotelId, CreateReviewCommand command)
    {
        command.HotelId = HotelId;
        var reviewId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetReviewsByHotelId), new { HotelId, reviewId }, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByHotelId([FromRoute] int HotelId)
    {
        var reviews = await mediator.Send(new GetHotelByIdQuery(HotelId));
        return Ok(reviews);
    }

}
