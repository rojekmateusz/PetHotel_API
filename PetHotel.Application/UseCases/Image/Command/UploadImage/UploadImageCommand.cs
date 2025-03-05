using MediatR;
using Microsoft.AspNetCore.Http;

namespace PetHotel.Application.UseCases.Image.Command.UploadImage;

public class UploadImageCommand : IRequest<int>
{
    public IFormFile? File { get; set; }
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int HotelId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
