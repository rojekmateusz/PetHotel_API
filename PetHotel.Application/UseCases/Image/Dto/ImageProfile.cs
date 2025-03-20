using AutoMapper;
using PetHotel.Application.UseCases.Image.Command.UploadImage;

namespace PetHotel.Application.UseCases.Image.Dto;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Domain.Entities.Image, ImageDto>();
        CreateMap<UploadImageCommand, Domain.Entities.Image>();
    }
}
