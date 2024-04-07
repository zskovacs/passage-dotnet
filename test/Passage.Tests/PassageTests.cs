using FluentAssertions;
using Moq;
using Moq.Contrib.HttpClient;
using Passage.Configuration;
using Passage.Exceptions;
using Passage.OpenApi;
using System.Net;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Passage.Tests;

public class PassageTests
{
    [Fact]
    public void Initialization_NoConfig_Should_ThrowException()
    {
        // Arrange

        // Act
        var act = () => { _ = new Passage(null); };

        // Assert
        act.Should().Throw<PassageException>().WithMessage(Errors.Config.MissingAppId);
    }

    [Fact]
    public void Initialization_NoAppId_Should_ThrowException()
    {
        // Arrange
        var config = new PassageConfig();

        // Act
        var act = () => { _ = new Passage(config); };

        // Assert
        act.Should().Throw<PassageException>().WithMessage(Errors.Config.MissingAppId);
    }

    [Fact]
    public void Initialization_Ok()
    {
        // Arrange
        var config = new PassageConfig() { AppId = "xxx" };

        // Act
        var act = () => { _ = new Passage(config); };

        // Assert
        act.Should().NotThrow();
    }
    
    [Fact]
    public void Initialization_Component_App_Initialized()
    {
        // Arrange
        var config = new PassageConfig() { AppId = "xxx" };

        // Act
        var passage = new Passage(config);

        // Assert
        passage.App.Should().NotBeNull();
    }
    
    [Fact]
    public void Initialization_Component_User_Initialized()
    {
        // Arrange
        var config = new PassageConfig() { AppId = "xxx" };

        // Act
        var passage = new Passage(config);

        // Assert
        passage.User.Should().NotBeNull();
    }
    
    [Fact]
    public void Initialization_Component_Session_Initialized()
    {
        // Arrange
        var config = new PassageConfig() { AppId = "xxx" };

        // Act
        var passage = new Passage(config);

        // Assert
        passage.Session.Should().NotBeNull();
    }
}