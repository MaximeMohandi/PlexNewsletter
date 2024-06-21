using MediahubNewsletter.Catalog;
using MediahubNewsletter.MediaLibrary;
using Moq;

namespace MediahubNewsletter.Tests.MediaLibrary;

public class MediaLibraryTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ShouldFetchOnlyMediaFromToday()
    {
        var expected = new List<Media>
        {
            new Media
            {
                Title = "Sharknado", Type = "movie", AddedAt = DateTime.Today,
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new Media
            {
                TvShow = "The Mandalorian", Season = 1, Episode = 2, Type = "tv show", AddedAt = DateTime.Today,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.", Title = "Chapter 1"
            }
        };
        var catalogMedias = new List<Media>
        {
            new Media
            {
                Title = "Sharknado", Type = "movie", AddedAt = DateTime.Today,
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new Media
            {
                Title = "The Hobbit: The Battle of the Five Armies", Type = "movie", AddedAt = new DateTime(2024,6,8, hour: 16, minute: 53, second: 59),
                Summary = "After the Dragon leaves the Lonely Mountain, the people of Lake-town see a threat coming. Orcs, dwarves, elves and people prepare for war. Bilbo sees Thorin going mad and tries to help. Meanwhile, Gandalf is rescued from the Necromancer's prison and his rescuers realize who the Necromancer is.",
            },
            new Media
            {
                TvShow = "The Mandalorian", Season = 1, Episode = 2, Type = "tv show", AddedAt = DateTime.Today,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.", Title = "Chapter 1"
            }
        };
        var catalog = new Mock<ICatalog>();
        catalog.Setup(c => c.Medias()).ReturnsAsync(catalogMedias);
        var mediaLibrary = new MediahubNewsletter.MediaLibrary.MediaLibrary(catalog.Object);

        var result = await mediaLibrary.RecentlyAddedMedia();

        CollectionAssert.AreEqual(expected, result);
    }
}
