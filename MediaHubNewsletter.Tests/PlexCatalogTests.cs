using System.Net;
using MediahubNewsletter;
using MediahubNewsletter.Catalog;
using MediahubNewsletter.MediaLibrary;

namespace PlexNewsletter.Tests;

public class PlexCatalogTests
{
    [Test]
    public async Task ShouldFetchMoviesFromCatalog()
    {
        var expected = new List<Media>
        {
            new Media
            {
                Title = "Sharknado", Type = "movie", AddedAt = new DateTime(2024,6,10, hour:20, minute: 42, second: 21),
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new Media
            {
                Title = "The Hobbit: The Battle of the Five Armies", Type = "movie", AddedAt = new DateTime(2024,6,8, hour: 16, minute: 53, second: 59),
                Summary = "After the Dragon leaves the Lonely Mountain, the people of Lake-town see a threat coming. Orcs, dwarves, elves and people prepare for war. Bilbo sees Thorin going mad and tries to help. Meanwhile, Gandalf is rescued from the Necromancer's prison and his rescuers realize who the Necromancer is.",
            },
        };
        var apiResponse = await File.ReadAllTextAsync("mockedPlexResponseOnlyMovies.json");
        var client = MockHttpRequestHandler.MockResponse(HttpStatusCode.OK, apiResponse);
        var catalog = new PlexCatalog(client);

        var result = await catalog.Medias();

        Assert.That(result, Is.EquivalentTo(expected));
    }

    [Test]
    public async Task ShouldFetchShowsFromCatalog()
    {
        var expected = new List<Media>
        {
            new Media
            {
                Title = "Inflation", Type = "show", AddedAt = new DateTime(2024,6,15, hour: 9, minute: 40, second: 24),
                Summary = "",
                TvShow = "My Hero Academia", Season = 7, Episode = 7

            },
            new Media
            {
                Title = "To Thine Own Self", Type = "show", AddedAt = new DateTime(2024,6,14, hour: 17, minute: 26, second: 43),
                Summary = "Tariq and Brayden struggle to adjust to life as normal, broke college students; Monet wants back into the game and answers about who took shots at her; Noma seeks out an opportunity to expand with the Russians.",
                TvShow = "Power Book II: Ghost", Season = 4, Episode = 2
            },
        };
        var apiResponse = await File.ReadAllTextAsync("mockedPlexResponseOnlyShows.json");
        var client = MockHttpRequestHandler.MockResponse(HttpStatusCode.OK, apiResponse);
        var catalog = new PlexCatalog(client);

        var result = await catalog.Medias();

        Assert.That(result, Is.EquivalentTo(expected));
    }
}
