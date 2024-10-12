using System.Net;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Newsletter.Discord;
using MediahubNewsletter.Tests.MediaLibrary.Mocks;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

namespace MediahubNewsletter.Tests.Newsletter.Discord;

public class DiscordDistributionTests
{
    private Mock<IConfiguration> _configuration;

    [SetUp]
    public void Setup()
    {
        _configuration = new Mock<IConfiguration>();
        _configuration.Setup(x => x["DiscordClient:webhookUrl"]).Returns("http://discord.com");
    }

    [Test]
    public async Task ShouldSendMessageInJsonToDiscord()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
            });
        var client = new HttpClient(mockHttpMessageHandler.Object);
        var mediaLibrary = new Mock<IMediaLibrary>();
        mediaLibrary.Setup(x => x.RecentlyAddedMedia()).ReturnsAsync(new List<FakeMedia>());
        // Act

        var discordDistribution = new DiscordDistribution(mediaLibrary.Object, client, _configuration.Object);
        await discordDistribution.Send();

        // Assert
        mockHttpMessageHandler.Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri == new Uri("http://discord.com/") &&
                    req.Content.Headers.ContentType.MediaType == "application/json" &&
                   req.Content.ReadAsStringAsync().Result == "{\"content\":\"\"}"
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }
}
