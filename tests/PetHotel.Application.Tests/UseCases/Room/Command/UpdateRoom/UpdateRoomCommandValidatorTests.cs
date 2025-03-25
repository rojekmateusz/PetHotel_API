using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using PetHotel.Application.UseCases.Room.Command.UpdateRoom;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Room.Command.UpdateRoom
{
    public class UpdateRoomCommandValidatorTests
    {
        [Theory()]
        [InlineData("Available")]
        [InlineData("Unavailable")]
        public void Validator_ForValidIsAvailablePropoerty_ShouldNotHavAnyValidatorPropertyErrors(string status)
        {
            // arrange

            var command = new UpdateRoomCommand
            {
                IsAvailable = status
            };

            var validator = new UpdateRoomCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveValidationErrorFor(r => r.IsAvailable);
        }

        [Theory()]
        [InlineData("available")]
        [InlineData("unavailable")]
        public void Validator_ForInValidIsAvailablePropoerty_ShouldHaveValidatorPropertyErrors(string status)
        {
            // arrange
            var command = new UpdateRoomCommand
            {
                IsAvailable = status
            };

            var validator = new UpdateRoomCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(r => r.IsAvailable);
        }
    }
}