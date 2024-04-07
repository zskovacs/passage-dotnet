using FluentAssertions;
using Passage.Configuration;
using Passage.Exceptions;
using Passage.OpenApi;

namespace Passage.Tests;

public class PassageExceptionTests
{
    /// <summary>
    /// Determines whether the status code of a PassageException object should inherit from the ApiException object.
    /// </summary>
    /// <remarks>
    /// This method verifies that the status code of a PassageException object is correctly inherited from the ApiException object.
    /// It sets up a test scenario by creating an ApiException object with a specified status code and message.
    /// Then, it creates a PassageException object with an error message and the ApiException object as its inner exception.
    /// Finally, it asserts that the StatusCode property of the PassageException object is equal to the status code specified in the ApiException object.
    /// If the assertion fails, it means that the status code of the PassageException object did not inherit from the ApiException object correctly.
    /// </remarks>
    [Fact]
    public void Should_StatusCode_Inherited_From_ApiException()
    {
        // Arrange
        const string message = "Test message";
        const int statusCode = 400;
        var apiExeption = new ApiException(message, statusCode, null, null, null);
        
        // Act
        var passageException = new PassageException("ERROR", apiExeption);
        
        // Assert
        passageException.StatusCode.Should().Be(statusCode);
    }

}