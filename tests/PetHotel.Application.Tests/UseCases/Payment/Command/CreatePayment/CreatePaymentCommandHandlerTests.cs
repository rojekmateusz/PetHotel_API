
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.Application.UseCases.Animal.Command.CreateAnimal;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Payment.Command.CreatePayment;

public class CreatePaymentCommandHandlerTests
{
    private readonly Mock<ILogger<CreatePaymentCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IOwnerRepository> _ownerRepositoryMock;
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly Mock<IOwnerAuthorizationService> _ownerAuthorizationServiceMock;
    private readonly CreatePaymentCommandHandler _handler;

    public CreatePaymentCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreatePaymentCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _ownerRepositoryMock = new Mock<IOwnerRepository>();
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _ownerAuthorizationServiceMock = new Mock<IOwnerAuthorizationService>();
        _handler = new CreatePaymentCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _ownerRepositoryMock.Object,
            _paymentRepositoryMock.Object,
            _ownerAuthorizationServiceMock.Object);
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedPaymentId()
    {
        // arrange

        var command = new CreatePaymentCommand
        {
            OwnerId = 1
        };

        var owner = new Domain.Entities.Owner();
        var payment = new Domain.Entities.Payment();

        _ownerRepositoryMock.Setup(repo => repo.GetOwnerByIdAsync(command.OwnerId))
            .ReturnsAsync(owner);

        _mapperMock.Setup(m => m.Map<Domain.Entities.Payment>(command))
            .Returns(payment);

        _paymentRepositoryMock.Setup(repo => repo.CreatePayment(It.IsAny<Domain.Entities.Payment>()))
            .ReturnsAsync(1);

        _ownerAuthorizationServiceMock.Setup(service => service.Authorize(owner, ResourceOperation.Create))
            .Returns(true);
        
        // act

        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
                
        result.Should().Be(1);
    }

    [Fact]
    public async Task Handle_WithNonExistingOwner_ShouldThrowNotFoundException()
    {
        // Arrange

        var request = new CreatePaymentCommand
        {
            OwnerId = 2
        };

        _ownerRepositoryMock.Setup(r => r.GetOwnerByIdAsync(request.OwnerId))
                .ReturnsAsync((Domain.Entities.Owner?)null);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // assert

        await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Owner with id: {request.OwnerId} doesn't exist");
    }

    [Fact]
    public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
    {
        // / Arrange
        var ownerId = 3;
        var request = new CreatePaymentCommand
        {
            OwnerId = ownerId
        };

        var existingOwner = new Domain.Entities.Owner
        {
            Id = ownerId
        };

        _ownerRepositoryMock
            .Setup(r => r.GetOwnerByIdAsync(ownerId))
                .ReturnsAsync(existingOwner);

        _ownerAuthorizationServiceMock
            .Setup(a => a.Authorize(existingOwner, ResourceOperation.Create))
                .Returns(false);

        // act

        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbidException>();
    }
}