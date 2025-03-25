using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Image.Command.UploadImage;
using PetHotel.Application.UseCases.Image.Dto;
using PetHotel.Domain.Constants;

namespace PetHotel.API.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ImageController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [Route("/hotels/{hotelId}/images")]
        public async Task<IActionResult> UploadImage([FromRoute] int hotelId, IFormFile file, string description)
        {
            using var stream = file.OpenReadStream();

            var command = new UploadImageCommand()
            {
                HotelId = hotelId,
                FileName = file.FileName, 
                File = stream
            };

            await mediator.Send(command);
            return NoContent();
          
        }
    }
}
