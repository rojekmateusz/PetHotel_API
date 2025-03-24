using Xunit;
using PetHotel.Application.UseCases.Payment.Command.CreatePayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace PetHotel.Application.UseCases.Payment.Command.CreatePayment.Tests
{
    public class CreatePaymentCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidatorErrors()
        {
            // arrange

            var command = new CreatePaymentCommand()
            {
                Amount = 100,
                Status = "Paid"
            };

            var validator = new CreatePaymentCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidatorErrors()
        {
            // arrange

            var command = new CreatePaymentCommand()
            {
                Amount = -10,
                Status = "x"
            };

            var validator = new CreatePaymentCommandValidator();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(r => r.Amount);
            result.ShouldHaveValidationErrorFor(r => r.Status);
        }

        [Theory]
        [InlineData("Paiid")]
        [InlineData("canceled")]
        [InlineData("unpaidd")]

        public void Validator_ForInValidStatus_ShouldHaveValidatorErrors(string status)
        {
            // arrange
            
            var validator = new CreatePaymentCommandValidator();
            var command = new CreatePaymentCommand { Status = status };

            // act
           
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Status);
        }
    }
}