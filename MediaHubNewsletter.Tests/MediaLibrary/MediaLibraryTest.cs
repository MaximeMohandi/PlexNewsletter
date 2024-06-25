using MediahubNewsletter.Catalog;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Tests.MediaLibrary.Mocks;
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
        var expected = new FakeMedia[]
        {
            new()
            {
                Title = "Sharknado", Type = "movie", AddedAt = DateTime.Today,
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new()
            {
                TvShow = "The Mandalorian", Season = 1, Episode = 2, Type = "tv show", AddedAt = DateTime.Today,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.", Title = "Chapter 1"
            }
        };
        var catalogMedias = FakeMedia.GetFakeMedias();
        var catalog = new Mock<ICatalog>();
        catalog.Setup(c => c.Medias()).ReturnsAsync(catalogMedias);
        var mediaLibrary = new MediahubNewsletter.MediaLibrary.MediaLibrary(catalog.Object);

        var result = await mediaLibrary.RecentlyAddedMedia();

        CollectionAssert.AreEqual(expected, result);
    }
}
