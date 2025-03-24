using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Owner.Command.UpdateOwner;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;
using PetHotel.Application.UseCases.Payment.Command.UpdatePayment;
using PetHotel.Application.UseCases.Payment.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Payment.Dto;

public class PaymentProfileTests
{
    private IMapper _mapper;
    public PaymentProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<PaymentProfile>();
        });

        _mapper = configuration.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromPaymentToPaymentDto_MapsCorrectly()
    {
        // arrange

        var payment = new Domain.Entities.Payment
        {
            Id = 1,
            Amount = 100,
            PaymentDate = DateTime.Now,
            Status = "Status Test",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            OwnerId = 1
        };

        // act

        var paymentDto = _mapper.Map<PaymentDto>(payment);

        // assert

        paymentDto.Should().NotBeNull();
        paymentDto.Id.Should().Be(payment.Id);
        paymentDto.Amount.Should().Be(payment.Amount);
        paymentDto.PaymentDate.Should().Be(payment.PaymentDate);
        paymentDto.Status.Should().Be(payment.Status);
        paymentDto.CreatedAt.Should().Be(payment.CreatedAt);
        paymentDto.UpdatedAt.Should().Be(payment.UpdatedAt);
        paymentDto.OwnerId.Should().Be(payment.OwnerId);
    }

    [Fact()]
    public void CreateMap_FromUpdatePaymentToPayment_MapsCorrectly()
    {
        // arrange

        var command = new UpdatePaymentCommand
        {
            Id = 1,
            Amount = 100,
            PaymentDate = DateTime.Now,
            Status = "Status Test",
            UpdatedAt = DateTime.Now,
            OwnerId = 1
        };

        // act

        var payment = _mapper.Map<Domain.Entities.Payment>(command);

        // assert

        payment.Should().NotBeNull();
        payment.Id.Should().Be(command.Id);
        payment.Amount.Should().Be(command.Amount);
        payment.PaymentDate.Should().Be(command.PaymentDate);
        payment.Status.Should().Be(command.Status);
        payment.UpdatedAt.Should().Be(command.UpdatedAt);
        payment.OwnerId.Should().Be(command.OwnerId);
    }

    [Fact()]
    public void CreateMap_FromCreatePaymentToPayment_MapsCorrectly()
    {
        // arrange

        var command = new CreatePaymentCommand
        {
            Amount = 100,
            PaymentDate = DateTime.Now,
            Status = "Status Test",
            CreatedAt = DateTime.Now,
            OwnerId = 1
        };

        // act

        var payment = _mapper.Map<Domain.Entities.Payment>(command);

        // assert

        payment.Should().NotBeNull();
        payment.Amount.Should().Be(command.Amount);
        payment.PaymentDate.Should().Be(command.PaymentDate);
        payment.Status.Should().Be(command.Status);
        payment.CreatedAt.Should().Be(command.CreatedAt);
        payment.OwnerId.Should().Be(command.OwnerId);
    }


}