using AutoMapper;
using FluentAssertions;
using PetHotel.Application.UseCases.Review.Command.CreateReview;
using PetHotel.Application.UseCases.Review.Command.UpdateReview;
using PetHotel.Application.UseCases.Review.Dto;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Review.Dto;

public class ReviewProfileTests
{
    private IMapper _mapper;
    public ReviewProfileTests()
    {
        var configure = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ReviewProfile>();
        });

        _mapper = configure.CreateMapper();
    }

    [Fact()]
    public void CreateMap_FromReviewToReviewDto_MapsCorrectly()
    {
        // arrange

        var review = new Domain.Entities.Review
        {
            Id = 1,
            Rating = 5,
            Comment = "Comment Test",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            HotelId = 1
        };

        // act

        var reviewDto = _mapper.Map<ReviewDto>(review);

        // assert

        reviewDto.Should().NotBeNull();
        reviewDto.Id.Should().Be(review.Id);
        reviewDto.Rating.Should().Be(review.Rating);
        reviewDto.Comment.Should().Be(review.Comment);
        reviewDto.CreatedAt.Should().Be(review.CreatedAt);
        reviewDto.UpdatedAt.Should().Be(review.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromUpdateReviewToReview_MapsCorrectly()
    {
        // arrange

        var command = new UpdateReviewCommand
        {
            Id = 1,
            Rating = 5,
            Comment = "Comment Test",
            UpdatedAt = DateTime.Now,
            HotelId = 1
        };

        // act

        var review = _mapper.Map<Domain.Entities.Review>(command);

        // assert

        review.Should().NotBeNull();
        review.Id.Should().Be(command.Id);
        review.Rating.Should().Be(command.Rating);
        review.Comment.Should().Be(command.Comment);
        review.UpdatedAt.Should().Be(command.UpdatedAt);
    }

    [Fact()]
    public void CreateMap_FromCreateReviewToReview_MapsCorrectly()
    {
        // arrange

        var command = new CreateReviewCommand
        {

            Rating = 5,
            Comment = "Comment Test",
            CreatedAt = DateTime.Now,
            HotelId = 1
        };

        // act

        var review = _mapper.Map<Domain.Entities.Review>(command);

        // assert

        review.Should().NotBeNull();
        review.Rating.Should().Be(command.Rating);
        review.Comment.Should().Be(command.Comment);
        review.CreatedAt.Should().Be(command.CreatedAt);
    }
}