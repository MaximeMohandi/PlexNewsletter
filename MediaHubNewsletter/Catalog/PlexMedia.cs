using System.Text.Json.Serialization;

namespace MediahubNewsletter.Catalog;

public record PlexMedia()
{
    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("addedAt")]
    public int AddedAtTimeStamp { get; init; }

    [JsonPropertyName("summary")]
    public string Summary { get; init; }

    [JsonPropertyName("grandparentTitle")]
    public string TvShow { get; init; } = string.Empty;

    [JsonPropertyName("parentIndex")]
    public int Season { get; init; }

    [JsonPropertyName("index")]
    public int Episode { get; init; }

    public DateTime AddedAt => DateTimeOffset.FromUnixTimeSeconds(AddedAtTimeStamp).DateTime;
};
