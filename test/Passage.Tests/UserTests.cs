using FluentAssertions;
using Moq;
using Moq.Contrib.HttpClient;
using Passage.Configuration;
using Passage.Exceptions;
using Passage.OpenApi;
using System.Net;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Passage.Tests;

public class UserTests
{
    [Fact]
    public void Initialization_NoConfig_Should_ThrowException()
    {
        // Arrange

        // Act
        var act = () => { _ = new User(null); };

        // Assert
        act.Should().Throw<PassageException>().WithMessage(Errors.Config.MissingAppId);
    }

    [Fact]
    public void Initialization_NoAppId_Should_ThrowException()
    {
        // Arrange
        var config = new PassageConfig();

        // Act
        var act = () => { _ = new User(config); };

        // Assert
        act.Should().Throw<PassageException>().WithMessage(Errors.Config.MissingAppId);
    }

    [Fact]
    public void Initialization_Ok()
    {
        // Arrange
        var config = new PassageConfig() { AppId = "xxx" };

        // Act
        var act = () => { _ = new User(config); };

        // Assert
        act.Should().NotThrow();
    }
}