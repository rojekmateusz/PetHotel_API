using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Owner.Command.CreateOwner;
using PetHotel.Application.UseCases.Owner.Command.UpdateOwner;
using PetHotel.Application.UseCases.Owner.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Owner.Dto;

public class OwnerProfileTests
{
    private IMapper _mapper;
    public OwnerProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OwnerProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromOwnerToOwnerDto_MapsCorrectly()
    {
        // arrange

        var owner = new Domain.Entities.Owner
        {
            Id = 1,
            FirstName = "First Name Test",
            LastName = "Last Name Test",
            PhoneNumber = "Phone Number Test",
            Email = "Email Test",
            Address = "Address Test",
            City = "City Test",
            PostalCode = "PostalCode Test",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        // act  

        var ownerDto = _mapper.Map<OwnerDto>(owner);

        // assert

        ownerDto.Should().NotBeNull();
        ownerDto.Id.Should().Be(owner.Id);
        ownerDto.FirstName.Should().Be(owner.FirstName);
        ownerDto.LastName.Should().Be(owner.LastName);
        ownerDto.PhoneNumber.Should().Be(owner.PhoneNumber);
        ownerDto.Email.Should().Be(owner.Email);
        ownerDto.Address.Should().Be(owner.Address);
        ownerDto.City.Should().Be(owner.City);
        ownerDto.PostalCode.Should().Be(owner.PostalCode);
        ownerDto.CreatedAt.Should().Be(owner.CreatedAt);
        ownerDto.UpdatedAt.Should().Be(owner.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromUpdateOwnerToOwner_MapsCorrectly()
    {
        // arrange

        var command = new UpdateOwnerCommand
        {
            Id = 1,
            FirstName = "First Name Test",
            LastName = "Last Name Test",
            PhoneNumber = "Phone Number Test",
            Email = "Email Test",
            Address = "Address Test",
            City = "City Test",
            PostalCode = "PostalCode Test",
            UpdatedAt = DateTime.Now,
        };

        // act  

        var owner = _mapper.Map<Domain.Entities.Owner>(command);

        // assert

        owner.Should().NotBeNull();
        owner.Id.Should().Be(command.Id);
        owner.FirstName.Should().Be(command.FirstName);
        owner.LastName.Should().Be(command.LastName);
        owner.PhoneNumber.Should().Be(command.PhoneNumber);
        owner.Email.Should().Be(command.Email);
        owner.Address.Should().Be(command.Address);
        owner.City.Should().Be(command.City);
        owner.PostalCode.Should().Be(command.PostalCode);
        owner.UpdatedAt.Should().Be(command.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromCreateOwnerToOwner_MapsCorrectly()
    {
        // arrange

        var command = new CreateOwnerCommand
        {

            FirstName = "First Name Test",
            LastName = "Last Name Test",
            PhoneNumber = "Phone Number Test",
            Email = "Email Test",
            Address = "Address Test",
            City = "City Test",
            PostalCode = "PostalCode Test",
            CreatedAt = DateTime.Now,
        };

        // act  

        var owner = _mapper.Map<Domain.Entities.Owner>(command);

        // assert

        owner.Should().NotBeNull();
        owner.FirstName.Should().Be(command.FirstName);
        owner.LastName.Should().Be(command.LastName);
        owner.PhoneNumber.Should().Be(command.PhoneNumber);
        owner.Email.Should().Be(command.Email);
        owner.Address.Should().Be(command.Address);
        owner.City.Should().Be(command.City);
        owner.PostalCode.Should().Be(command.PostalCode);
        owner.CreatedAt.Should().Be(command.CreatedAt);
    }
}