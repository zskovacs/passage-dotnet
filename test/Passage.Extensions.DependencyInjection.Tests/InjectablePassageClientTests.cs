using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Passage.Configuration;
using System.Net.Http.Headers;
using System.Reflection;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Passage.Extensions.DependencyInjection.Tests;


public class InjectablePassageClientTests
{
    [Fact]
    public void Constructor_Should_Setup_HttpClient_Headers()
    {
        // Arrange
        var apiKey = "test_api_key";
        var appId = "test_app_id";
        
        var config = new PassageConfig { ApiKey = apiKey, AppId = appId };
        var mockOptions = new Mock<IOptions<PassageConfig>>();
        mockOptions.Setup(o => o.Value).Returns(config);

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(mockHttpMessageHandler.Object);

        // Act
        var injectablePassageClient = new InjectablePassageClient(mockOptions.Object, httpClient);

        // Assert
        httpClient.DefaultRequestHeaders.Authorization.Should().BeEquivalentTo(new AuthenticationHeaderValue("Bearer", apiKey));
        httpClient.DefaultRequestHeaders.GetValues("Passage-Version").Should().ContainSingle()
            .Which.Should().Be(Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }
}