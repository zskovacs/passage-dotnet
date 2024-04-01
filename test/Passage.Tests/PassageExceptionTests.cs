using FluentAssertions;
using Passage.Exceptions;
using Passage.OpenApi;

namespace Passage.Tests;

public class PassageExceptionTests
{
    [Fact]
    public void Should_StatusCode_Inherit_From_ApiException()
    {
        // Arrange
        const string message = "Test message";
        const int statusCode = 400;
        var apiExeption = new ApiException(message, statusCode, null, null, null);
        
        // Act
        var passageException = new PassageException("ERROR", apiExeption);
        
        // Assert
        passageException.StatusCode.Should().Be(statusCode);
        passageException.Error.Should().StartWith(message);
    }

}