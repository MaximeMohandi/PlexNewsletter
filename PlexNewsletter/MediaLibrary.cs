namespace MediahubNewsletter;

public class MediaLibrary
{
    public static List<Media> RecentlyAddedMedia()
    {
        return new List<Media>
        {
            new Media { Title = "The Matrix", type = "Movie", AddedAt = DateTime.Now.Date, Summary = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers."},
            new Media { Title = "The Matrix Reloaded", type = "Movie", AddedAt = DateTime.Now.Date, Summary = "Neo and his allies"},
            new Media { Title = "The red wedding", type = "Episode", AddedAt = DateTime.Now.Date, Summary = "The Starks are dead", Season = "season 3", TvShow = "Game of Thrones"}
        };
    }
}
