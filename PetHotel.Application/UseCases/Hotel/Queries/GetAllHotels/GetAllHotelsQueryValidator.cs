using FluentValidation;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQueryValidator : AbstractValidator<GetAllHotelsQuery>
{
    private int[] allowPageSizes= { 5, 10, 20, 30 };

    public GetAllHotelsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");
    }
}
