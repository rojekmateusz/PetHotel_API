﻿using FluentValidation;

namespace PetHotel.Application.UseCases.Hotel.Command.UpdateHotel;

public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
{
    private readonly List<string> validIsActive = ["Active", "Inactive"];
    public UpdateHotelCommandValidator()
    {
        RuleFor(dto => dto.HotelName)
           .Length(2, 40)
           .WithMessage("Hotel Name must contain from 3 to 40 characters");

        RuleFor(dto => dto.HotelsNIP)
            .Length(10)
            .WithMessage("Hotel NIP must contain 9 numbers");

        RuleFor(dto => dto.Email)
            .EmailAddress()
            .WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.PostalCode)
           .Matches(@"^\d{2}-\d{3}$")
           .WithMessage("Please provide a valid postal code (XX-XXX).");

        RuleFor(dto => dto.IsActive)
            .Must(validIsActive.Contains)
            .WithMessage("Invalid status. Please choose from the valid categories. Active or Inactive");
    }
}
