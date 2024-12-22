using System.Net;
using MediahubNewsletter.Catalog;
using MediahubNewsletter.MediaLibrary;
using Microsoft.Extensions.Configuration;
using Moq;

namespace MediahubNewsletter.Tests.Catalog;

public class PlexCatalogTests
{
    private Mock<IConfiguration> _configuration;

    [SetUp]
    public void Setup()
    {
        _configuration = new Mock<IConfiguration>();
        _configuration.Setup(x => x["PlexClient:url"]).Returns("http://localhost:32400");
        _configuration.Setup(x => x["PlexClient:token"]).Returns("token");
    }

    [Test]
    public async Task ShouldConvertPlexMediaDateToDateTime()
    {
        PlexMedia plexMedia = new()
        {
            AddedAtTimeStamp = 1641024000 // 2022-01-01 08:00:00
        };

        Assert.That(plexMedia.AddedAt, Is.EqualTo(new DateTime(2022, 1, 1, 8, 0, 0)));
    }

    [Test]
    public async Task ShouldFetchPlexCatalog()
    {
        var expected = new PlexMedia[]
        {
            new()
            {
                Title = "Sharknado", Type = MediaType.Movie, AddedAtTimeStamp = 1718052141,
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new()
            {
                Title = "The Hobbit: The Battle of the Five Armies", Type = MediaType.Movie,
                AddedAtTimeStamp = 1717865639,
                Summary =
                    "After the Dragon leaves the Lonely Mountain, the people of Lake-town see a threat coming. Orcs, dwarves, elves and people prepare for war. Bilbo sees Thorin going mad and tries to help. Meanwhile, Gandalf is rescued from the Necromancer's prison and his rescuers realize who the Necromancer is."
            },
            new()
            {
                Title = "Inflation", Type = MediaType.TvShow, AddedAtTimeStamp = 1718444424,
                Summary = "",
                TvShow = "My Hero Academia", Season = 7, Episode = 7
            },
            new()
            {
                Title = "To Thine Own Self", Type = MediaType.TvShow, AddedAtTimeStamp = 1718386003,
                Summary =
                    "Tariq and Brayden struggle to adjust to life as normal, broke college students; Monet wants back into the game and answers about who took shots at her; Noma seeks out an opportunity to expand with the Russians.",
                TvShow = "Power Book II: Ghost", Season = 4, Episode = 2
            }
        };
        var apiResponse = await File.ReadAllTextAsync("Catalog\\Mocks\\mockedPlexResponse.Json");
        var client = MockHttpRequestHandler.MockResponse(HttpStatusCode.OK, apiResponse);

        var catalog = new PlexCatalog(client, _configuration.Object);

        var result = await catalog.Medias();

        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public async Task ShouldIgnoreInvalidMediaType()
    {
        var expected = new PlexMedia[]
        {
            new()
            {
                Title = "Inflation", Type = MediaType.TvShow, AddedAtTimeStamp = 1718444424,
                Summary = "",
                TvShow = "My Hero Academia", Season = 7, Episode = 7
            },
            new()
            {
                Title = "To Thine Own Self", Type = MediaType.TvShow, AddedAtTimeStamp = 1718386003,
                Summary =
                    "Tariq and Brayden struggle to adjust to life as normal, broke college students; Monet wants back into the game and answers about who took shots at her; Noma seeks out an opportunity to expand with the Russians.",
                TvShow = "Power Book II: Ghost", Season = 4, Episode = 2
            }
        };
        var apiResponse = await File.ReadAllTextAsync("Catalog\\Mocks\\mockedPlexResponseWithInvalidMediaType.Json");
        var client = MockHttpRequestHandler.MockResponse(HttpStatusCode.OK, apiResponse);
        var catalog = new PlexCatalog(client, _configuration.Object);

        var result = await catalog.Medias();

        CollectionAssert.AreEqual(expected, result);
    }

    [Test]
    public async Task ShouldReturnEmptyWhenNoMedia()
    {
        var apiResponse = await File.ReadAllTextAsync("Catalog\\Mocks\\mockedPlexResponseWithNoMedia.Json");
        var client = MockHttpRequestHandler.MockResponse(HttpStatusCode.OK, apiResponse);
        var catalog = new PlexCatalog(client, _configuration.Object);

        var result = await catalog.Medias();

        CollectionAssert.AreEqual(Array.Empty<IMedia>(), result);
    }
}
