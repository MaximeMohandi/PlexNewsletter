namespace MediahubNewsletter;

public record Media
{
    public string Title { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public DateTime AddedAt { get; init; }
    public string Summary { get; init; } = string.Empty;
    public string Season { get; init; } = string.Empty;
    public string TvShow { get; init; } = string.Empty;
}
