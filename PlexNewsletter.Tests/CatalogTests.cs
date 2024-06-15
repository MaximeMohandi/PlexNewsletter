using System.Net;
using MediahubNewsletter;

namespace PlexNewsletter.Tests;

public class CatalogTests
{
    [Test]
    public async Task ShouldFetchMediaFromCatalog()
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
        var apiResponse = await File.ReadAllTextAsync("mockedPlexResponse.json");
        var client = MockHttpRequestHandler.MockResponse(HttpStatusCode.OK, apiResponse);
        var catalog = new Catalog(client);

        var result = await catalog.Medias();

        Assert.That(result, Is.EquivalentTo(expected));
    }
}
