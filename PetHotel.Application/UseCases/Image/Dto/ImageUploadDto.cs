using Microsoft.AspNetCore.Http;

namespace PetHotel.Application.UseCases.Image.Dto;

public class ImageUploadDto
{
    public IFormFile? File { get; set; }
}
