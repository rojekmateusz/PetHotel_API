using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using PetHotel.API.Middlewares;
using PetHotel.Domain.Exceptions;
using Xunit;

namespace PetHotel.API.Tests.Middlewares;

public class ErrorHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldThrownNextDelegate()
    {
        // arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMoq = new Mock<RequestDelegate>();

        // act

        await middleware.InvokeAsync(context, nextDelegateMoq.Object);

        // assert

        nextDelegateMoq.Verify(x => x.Invoke(context), Times.Once);

    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldReturnStatusCode404()
    {
        // arrange

        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var context = new DefaultHttpContext();
        var exception = new NotFoundException(nameof(Domain.Entities.Hotel), "1");

        // act

        await middleware.InvokeAsync(context, _ => throw exception);

        // assert

        context.Response.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
    {
        // arrange

        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var exception = new ForbidException();

        // act

        await middleware.InvokeAsync(context, _ => throw exception);

        // Assert

        context.Response.StatusCode.Should().Be(403);

    }

    [Fact]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
    {
        // arrange

        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
        var exception = new Exception();

        // act

        await middleware.InvokeAsync(context, _ => throw exception);

        // assert

        context.Response.StatusCode.Should().Be(500);

    }

}