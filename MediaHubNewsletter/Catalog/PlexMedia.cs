using System.Text.Json.Serialization;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public record PlexMedia : IMedia
{
    [JsonPropertyName("addedAt")] public long AddedAtTimeStamp { get; init; }

    [JsonPropertyName("title")] public string Title { get; init; }

    [JsonConverter(typeof(PlexMediaTypeJsonConverter))]
    [JsonPropertyName("type")]
    public MediaType Type { get; init; }

    [JsonPropertyName("summary")] public string Summary { get; init; }

    [JsonPropertyName("grandparentTitle")] public string TvShow { get; init; } = string.Empty;

    [JsonPropertyName("parentIndex")] public int Season { get; init; }

    [JsonPropertyName("index")] public int Episode { get; init; }

    public DateTime AddedAt => DateTimeOffset.FromUnixTimeSeconds(AddedAtTimeStamp).DateTime;
}
