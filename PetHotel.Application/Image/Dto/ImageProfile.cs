using AutoMapper;

namespace PetHotel.Application.Image.Dto;

public class ImageProfile : Profile
{
    public ImageProfile()
    { 
        CreateMap<Domain.Entities.Image,ImageDto>();    
    }
}
