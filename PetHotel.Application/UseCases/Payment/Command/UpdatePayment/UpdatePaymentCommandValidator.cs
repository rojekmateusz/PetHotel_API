using FluentValidation;

namespace PetHotel.Application.UseCases.Payment.Command.UpdatePayment;

public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    private readonly List<string> validStatus = ["Paid", "Cancelled", "Unpaid"];
    public UpdatePaymentCommandValidator()
    {
        RuleFor(dto => dto.Amount)
          .GreaterThanOrEqualTo(0)
          .WithMessage("The amount of the bill must be greater than 0.");

        RuleFor(dto => dto.Status)
            .Must(validStatus.Contains)
            .WithMessage("Invalid status. Please choose from the valid categories. Cancelled, Paid or Unpaid");
    }

    public void ShouldHaveValidationErrorFor(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
