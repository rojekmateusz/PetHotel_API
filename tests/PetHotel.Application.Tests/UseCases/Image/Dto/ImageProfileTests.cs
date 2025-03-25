using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Image.Command.UploadImage;
using PetHotel.Application.UseCases.Image.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Image.Dto;

public class ImageProfileTests
{
    private IMapper _mapper;
    public ImageProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ImageProfile>();
        });

        _mapper = new Mapper(configuration);
    }

    [Fact()]
    public void CreateMap_FromImageToImageDto_MapsCorrectly()
    {
        // arrange

        var image = new Domain.Entities.Image
        {
            Id = 1,
            Url = "Url Test",
            HotelId = 1,
            Description = "Description Test",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        // act

        var imageDto = _mapper.Map<ImageDto>(image);

        // assert

        imageDto.Should().NotBeNull();
        imageDto.Id.Should().Be(image.Id);
        imageDto.Url.Should().Be(image.Url);
        imageDto.HotelId.Should().Be(image.HotelId);
        imageDto.Description.Should().Be(image.Description);
        imageDto.CreatedAt.Should().Be(image.CreatedAt);
    }

    [Fact()]
    public void CreateMap_FromUploadImageCommandToImage_MapsCorrectly()
    {
        // arrange

        var command = new UploadImageCommand
        {
            Url = "Url Test",
            HotelId = 1,
            Description = "Description Test",
        };

        // act

        var image = _mapper.Map<Domain.Entities.Image>(command);

        // assert

        image.Should().NotBeNull();
        image.Url.Should().Be(command.Url);
        image.HotelId.Should().Be(command.HotelId);
        image.Description.Should().Be(command.Description);
    }
}