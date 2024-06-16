using System.Net.Http.Headers;
using System.Text.Json;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public class PlexCatalog : ICatalog
{
    private readonly HttpClient _client;
    private const string PlexUrl = "http://localhost:32400";
    private const string PlexToken = "token";

    public PlexCatalog(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Media>> Medias()
    {
        var plexRecentlyAddedMediaUri = new Uri($"{PlexUrl}/library/recentlyAdded?X-Plex-Token={PlexToken}");
        var response = await _client.GetAsync(plexRecentlyAddedMediaUri);

        return ParsePlexMedia(JsonDocument.Parse(await response.Content.ReadAsStringAsync()));
    }

    private static List<Media> ParsePlexMedia(JsonDocument plexCatalog)
    {
        var medias = new List<Media>();

        var root = plexCatalog.RootElement;
        var media = root.GetProperty("MediaContainer").GetProperty("Metadata");

        foreach (var item in media.EnumerateArray())
        {
            var title = item.GetProperty("title").GetString();
            var type = item.GetProperty("type").GetString();
            var addedAt = DateTime.UnixEpoch
                .AddSeconds(item.GetProperty("addedAt").GetInt32());
            var summary = item.GetProperty("summary").GetString();

            if (type == "episode")
            {
                medias.Add(new Media
                {
                    Title = title,
                    Type = "show",
                    AddedAt = addedAt,
                    Summary = summary,
                    TvShow = item.GetProperty("grandparentTitle").GetString(),
                    Season = item.GetProperty("parentIndex").GetInt32(),
                    Episode = item.GetProperty("index").GetInt32()
                });
            }
            else
            {
                medias.Add(new Media
                {
                    Title = title,
                    Type = type,
                    AddedAt = addedAt,
                    Summary = summary
                });
            }
        }
        return medias;
    }
}
