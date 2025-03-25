using MediatR;
using Microsoft.AspNetCore.Http;

namespace PetHotel.Application.UseCases.Image.Command.UploadImage;

public class UploadImageCommand : IRequest<int>
{
    public string FileName { get; set; } = default!;
    public Stream File { get; set; } = default!;
    public string? Url { get; set; }
    public string? Description { get; set; }
    public int HotelId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
  
}
