using Xunit;
using PetHotel.Application.UseCases.Room.Command.CreateRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace PetHotel.Application.UseCases.Room.Command.CreateRoom.Tests
{
    public class CreateRoomCommandValidatorTests
    {
        [Theory()]
        [InlineData("Available")]
        [InlineData("Unavailable")]
        public void Validator_ForValidIsAvailablePropoerty_ShouldNotHavAnyValidatorPropertyErrors(string status)
        {
            // arrange

            var command = new CreateRoomCommand
            {
                IsAvailable = status
            };

            var validator = new CreateRoomCommandValidator();

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
            var command = new CreateRoomCommand
            {
                IsAvailable = status
            };

            var validator = new CreateRoomCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(r => r.IsAvailable);
        }
    }
}