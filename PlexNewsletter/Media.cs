namespace MediahubNewsletter;

public record Media
{
    public string Title { get; init; }
    public string type { get; init; }
    public DateTime AddedAt { get; init; }
    public string Summary { get; init; }
    public string Season { get; init; } = string.Empty;
    public string TvShow { get; init; } = string.Empty;
}