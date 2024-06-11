namespace PlexNewsletter.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldFetchMediaListFromPlex()
    {
        var expected = new List<Media>
        {
            new Media { Title = "The Matrix", type = "Movie", AddedAt = DateTime.Now.Date, Summary = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers."},
            new Media { Title = "The Matrix Reloaded", type = "Movie", AddedAt = DateTime.Now.Date, Summary = "Neo and his allies"},
            new Media { Title = "The red wedding", type = "Episode", AddedAt = DateTime.Now.Date, Summary = "The Starks are dead", Season = "season 3", TvShow = "Game of Thrones"}
        };

        var result = new List<Media>
        {
            new Media { Title = "The Matrix", type = "Movie", AddedAt = DateTime.Now.Date, Summary = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers."},
            new Media { Title = "The Matrix Reloaded", type = "Movie", AddedAt = DateTime.Now.Date, Summary = "Neo and his allies"},
            new Media { Title = "The red wedding", type = "Episode", AddedAt = DateTime.Now.Date, Summary = "The Starks are dead", Season = "season 3", TvShow = "Game of Thrones"}
        };

        Assert.That(result, Is.EqualTo(expected));
    }
}

public record Media
{
    public string Title { get; init; }
    public string type { get; init; }
    public DateTime AddedAt { get; init; }
    public string Summary { get; init; }
    public string Season { get; init; } = string.Empty;
    public string TvShow { get; init; } = string.Empty;
}
