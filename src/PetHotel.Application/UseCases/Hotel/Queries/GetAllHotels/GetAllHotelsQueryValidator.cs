using FluentValidation;
using PetHotel.Application.UseCases.Hotel.Dto;

namespace PetHotel.Application.UseCases.Hotel.Queries.GetAllHotels;

public class GetAllHotelsQueryValidator : AbstractValidator<GetAllHotelsQuery>
{
    private int[] allowPageSizes= { 5, 10, 20, 30 };

    private string[] allowedSortByColumnNames = [nameof(HotelDto.HotelName),
         nameof(HotelDto.Description),
         nameof(HotelDto.City)];

    public GetAllHotelsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
             .Must(value => allowedSortByColumnNames.Contains(value))
             .When(q => q.SortBy != null)
             .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}
