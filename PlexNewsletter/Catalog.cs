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
        var plexMedias = await PlexMediaCatalog();
        return ParsePlexMedia(plexMedias);
    }

    private async Task<JsonDocument> PlexMediaCatalog()
    {
        var plexRecentlyAddedMediaUri = new Uri($"{PlexUrl}/library/recentlyAdded?X-Plex-Token={PlexToken}");
        var response = await _client.GetAsync(plexRecentlyAddedMediaUri);
        return  JsonDocument.Parse(await response.Content.ReadAsStringAsync());
    }

    private static List<Media> ParsePlexMedia(JsonDocument plexCatalog)
    {
        var medias = new List<Media>();

        var root = plexCatalog.RootElement;
        var media = root.GetProperty("MediaContainer").GetProperty("Metadata");

        foreach (var item in media.EnumerateArray())
        {
            medias.Add(new Media
            {
                Title = item.GetProperty("title").GetString(),
                Type = item.GetProperty("type").GetString(),
                AddedAt = DateTime.UnixEpoch
                    .AddSeconds(item.GetProperty("addedAt").GetInt32()),
                Summary = item.GetProperty("summary").GetString()
            });
        }
        return medias;
    }
}
