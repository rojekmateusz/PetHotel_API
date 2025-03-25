using Xunit;
using FluentAssertions;
using PetHotel.Domain.Constants;

namespace PetHotel.Application.User.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_ShouldReturnTrue(string role)
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User]);

            // act

            var isInRole = currentUser.IsInRole(role);

            // assert

            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            // arrange

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User]);

            // act

            var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

            // assert

            isInRole.Should().BeFalse();

        }


    }
}