using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotel.Application.UseCases.Image.Command.UploadImage;
using PetHotel.Application.UseCases.Image.Dto;

namespace PetHotel.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ImageController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("/hotels/{hotelId}/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage([FromRoute] int hotelId, IFormFile file, [FromBody] UploadImageCommand command)
        {
            command.HotelId = hotelId;
            command.File = file;
            var imageUrl = await mediator.Send(command);
            
            return Ok(new { imageUrl });
          
        }
    }
}
