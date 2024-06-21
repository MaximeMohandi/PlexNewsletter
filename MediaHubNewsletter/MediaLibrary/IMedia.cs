namespace MediahubNewsletter.MediaLibrary;

public interface IMedia
{
    string Title { get; init; }
    string Type { get; init; }
    DateTime AddedAt { get; init; }
    string Summary { get; init; }
    int Season { get; init; }
    string TvShow { get; init; }
    int Episode { get; init; }
}