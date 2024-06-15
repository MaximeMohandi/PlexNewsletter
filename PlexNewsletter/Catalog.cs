using System.Net.Http.Headers;
using System.Text.Json;

namespace MediahubNewsletter;

public class Catalog
{
    private readonly HttpClient _client;
    private const string PlexUrl = "http://localhost:32400";
    private const string PlexToken = "token";

    public Catalog(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Media>> Medias()
    {
        var mediaList = new List<Media>();
        using (JsonDocument doc = JsonDocument.Parse(FetchMediaFromPlex()))
        {
            var root = doc.RootElement;
            var media = root.GetProperty("MediaContainer").GetProperty("Metadata");

            foreach (var item in media.EnumerateArray())
            {
                mediaList.Add(ConvertToMedia(item));
            }
        }

        return mediaList;
    }

    private string FetchMediaFromPlex()
    {
        var plexRecentlyAddedMediaUri = new Uri($"{PlexUrl}/library/recentlyAdded?X-Plex-Token={PlexToken}");
        var response = _client.GetAsync(plexRecentlyAddedMediaUri).Result;
        var content = response.Content.ReadAsStringAsync().Result;
        return content;
    }

    private static Media ConvertToMedia(JsonElement item)
    {
        var title = item.GetProperty("title").GetString();
        var type = item.GetProperty("type").GetString();
        var addedAt = item.GetProperty("addedAt").GetUInt32();
        var summary = item.GetProperty("summary").GetString();
        return new Media
        {
            Title = title,
            Type = type,
            AddedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(addedAt),
            Summary = summary
        };
    }
}
