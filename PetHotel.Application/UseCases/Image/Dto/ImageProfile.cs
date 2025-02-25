using AutoMapper;

namespace PetHotel.Application.UseCases.Image.Dto;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Domain.Entities.Image, ImageDto>();
    }
}
