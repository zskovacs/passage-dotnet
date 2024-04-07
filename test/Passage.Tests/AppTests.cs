using FluentAssertions;
using Moq;
using Moq.Contrib.HttpClient;
using Passage.Configuration;
using Passage.Exceptions;
using Passage.OpenApi;
using System.Net;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Passage.Tests;

public class AppTests
{
    [Fact]
    public void Initialization_NoConfig_Should_ThrowException()
    {
        // Arrange

        // Act
        var act = () => { _ = new App(null); };

        // Assert
        act.Should().Throw<PassageException>().WithMessage(Errors.Config.MissingAppId);
    }

    [Fact]
    public void Initialization_NoAppId_Should_ThrowException()
    {
        // Arrange
        var config = new PassageConfig();

        // Act
        var act = () => { _ = new App(config); };

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

    #region GetApp
    [Fact]
    public async Task Get_Should_Return_AppInfo()
    {
        // Arrange
        var passageConfig = new PassageConfig() { AppId = "xxx" };
        var mockUrl = $"https://api.passage.id/v1/apps/{passageConfig.AppId}";
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        mockHandler.SetupRequest(HttpMethod.Get, mockUrl).ReturnsResponse(HttpStatusCode.OK, "{}");

        var httpClient = mockHandler.CreateClient();
        var sut = new App(passageConfig, httpClient);

        // Act
        var result = await sut.Get();
        
        // Assert
        mockHandler.VerifyRequest(HttpMethod.Get, mockUrl, Times.Once());
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Get_Should_ThrowApiException()
    {
        // Arrange
        var passageConfig = new PassageConfig() { AppId = "xxx" };
        var mockUrl = $"https://api.passage.id/v1/apps/{passageConfig.AppId}";
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        mockHandler.SetupRequest(HttpMethod.Get, mockUrl).ReturnsResponse(HttpStatusCode.NotFound, "{}");

        var httpClient = mockHandler.CreateClient();
        var sut = new App(passageConfig, httpClient);

        // Act
        var act = async () => await sut.Get();
        
        // Assert
        await act.Should().ThrowAsync<PassageException>()
            .Where(x => x.StatusCode == 404)
            .WithMessage(Errors.Client.ApiException)
            .WithInnerException(typeof(ApiException));
    }

    [Fact]
    public async Task Get_Should_ThrowException()
    {
        // Arrange
        var passageConfig = new PassageConfig() { AppId = "xxx" };
        var mockUrl = $"https://api.passage.id/v1/apps/{passageConfig.AppId}";
        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        mockHandler.SetupRequest(HttpMethod.Get, mockUrl).ThrowsAsync(new Exception());

        var httpClient = mockHandler.CreateClient();
        var sut = new App(passageConfig, httpClient);

        // Act
        var act = async () => await sut.Get();
        
        // Assert
        await act.Should().ThrowAsync<PassageException>()
            .Where(x => x.StatusCode == 500)
            .WithMessage(Errors.Client.UnexpectedError)
            .WithInnerException(typeof(Exception));
    }

    [Fact]
    public async Task Get_MissingApiKey_Should_ThrowException()
    {
        // Arrange
        var passageConfig = new PassageConfig() { AppId = "xxx" };
        var sut = new App(passageConfig);

        // Act
        var act = async () => await sut.Get();
        
        // Assert
        await act.Should().ThrowAsync<PassageException>().WithMessage(Errors.Config.MissingApiKey);
    }
    #endregion
}