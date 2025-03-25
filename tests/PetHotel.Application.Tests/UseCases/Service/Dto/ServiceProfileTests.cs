using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Service.Command.CreateService;
using PetHotel.Application.UseCases.Service.Command.UpdateService;
using PetHotel.Application.UseCases.Service.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Service.Dto;

public class ServiceProfileTests
{
    private IMapper _mapper;
    public ServiceProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceProfile>();
        });

        _mapper = new Mapper(configuration);
    }

    [Fact()]
    public void CreateMap_FromServiceToServiceDto_MapsCorrectly()
    {
        // arrange

        var service = new Domain.Entities.Service
        {
            ServiceId = 1,
            Name = "Service 1",
            Description = "Service 1 Description",
            Price = 10,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // act

        var serviceDto = _mapper.Map<ServiceDto>(service);

        // assert

        serviceDto.Should().NotBeNull();
        serviceDto.ServiceId.Should().Be(service.ServiceId);
        serviceDto.Name.Should().Be(service.Name);
        serviceDto.Description.Should().Be(service.Description);
        serviceDto.Price.Should().Be(service.Price);
        serviceDto.CreatedAt.Should().Be(service.CreatedAt);
        serviceDto.UpdatedAt.Should().Be(service.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromUpdateServiceToService_MapsCorrectly()
    {
        // arrange

        var command = new UpdateServiceCommand
        {
            ServiceId = 1,
            Name = "Service 1",
            Description = "Service 1 Description",
            Price = 10,
            UpdatedAt = DateTime.Now
        };

        // act

        var service = _mapper.Map<Domain.Entities.Service>(command);

        // assert

        service.Should().NotBeNull();
        service.ServiceId.Should().Be(command.ServiceId);
        service.Name.Should().Be(command.Name);
        service.Description.Should().Be(command.Description);
        service.Price.Should().Be(command.Price);
        service.UpdatedAt.Should().Be(command.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromCreateServiceToService_MapsCorrectly()
    {
        // arrange

        var command = new CreateServiceCommand
        {
            Name = "Service 1",
            Description = "Service 1 Description",
            Price = 10,
            CreatedAt = DateTime.Now
        };

        // act

        var service = _mapper.Map<Domain.Entities.Service>(command);

        // assert

        service.Should().NotBeNull();
        service.Name.Should().Be(command.Name);
        service.Description.Should().Be(command.Description);
        service.Price.Should().Be(command.Price);
        service.CreatedAt.Should().Be(command.CreatedAt);
    }
}