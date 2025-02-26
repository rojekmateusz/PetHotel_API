using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Hotel.Queries.GetHotelById;
using PetHotel.Application.UseCases.Review.Command.CreateReview;
using PetHotel.Application.UseCases.Review.Queries.GetReviewsByHotelId;
using PetHotel.Application.UseCases.Review.Dto;
using PetHotel.Application.UseCases.Review.Queries.GetReviewByIdForHotel;
using PetHotel.Application.UseCases.Review.Command.DeleteReview;
using PetHotel.Application.UseCases.Review.Command.UpdateReview;
namespace PetHotel.API.Controllers;

[ApiController]
[Route("api/hotels/{HotelId}/reviews")]
public class ReviewController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> CreateReview([FromRoute] int HotelId, CreateReviewCommand command)
    {
        command.HotelId = HotelId;
        var reviewId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetReviewsByHotelId), new { HotelId, reviewId }, null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsByHotelId([FromRoute] int HotelId)
    {
        var reviews = await mediator.Send(new GetHotelByIdQuery(HotelId));
        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewByIdForHotel([FromRoute] int reviewId, [FromRoute] int HotelId)
    {
        var review = await mediator.Send(new GetReviewByIdForHotelQuery(reviewId, HotelId));
        return Ok(review);
    }

    [HttpDelete("{reviewId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] int reviewId, [FromRoute] int HotelId)
    {
        await mediator.Send(new DeleteReviewCommand(reviewId, HotelId));
        return NoContent();
    }

    [HttpPatch("{reviewId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReview([FromRoute] int reviewId, [FromRoute] int HotelId, UpdateReviewCommand command)
    {
        command.Id = reviewId;
        command.HotelId = HotelId;
        await mediator.Send(command);
        return Ok();
    }
}
