using FluentValidation.TestHelper;
using PetHotel.Application.UseCases.Image.Command.UploadImage;
using Xunit;

namespace PetHotel.Application.Tests.UseCases.Image.Command.UploadImage
{
    public class UploadImageCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidatorErrors()
        {
            // arrange

            var command = new UploadImageCommand
            { 
                Description = "This is a valid description"
            };

            var validator = new UploadImageCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidatorErrors()
        {
            // arrange

            var command = new UploadImageCommand
            {
                Description = new string('a', 501)
            };

            var validator = new UploadImageCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Description);
        }
    }
}