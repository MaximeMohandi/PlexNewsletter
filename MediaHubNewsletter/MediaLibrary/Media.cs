namespace MediahubNewsletter.MediaLibrary;

public record Media
{
    public string Title { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public DateTime AddedAt { get; init; }
    public string Summary { get; init; } = string.Empty;
    public int Season { get; init; }
    public string TvShow { get; init; } = string.Empty;
    public int Episode { get; init; }
}
