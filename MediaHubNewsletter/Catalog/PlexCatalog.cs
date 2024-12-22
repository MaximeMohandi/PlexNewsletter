using System.Net.Http.Headers;
using System.Text.Json;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public class PlexCatalog : ICatalog
{
    private readonly HttpClient _client;
    private readonly string _plexToken;
    private readonly string _plexUrl;

    public PlexCatalog(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _plexUrl = configuration["PlexClient:url"];
        _plexToken = configuration["PlexClient:token"];
    }

    public async Task<IEnumerable<IMedia>> Medias()
    {
        var plexRecentlyAddedMediaUri = new Uri($"{_plexUrl}/library/recentlyAdded?X-Plex-Token={_plexToken}");
        var response = await _client.GetAsync(plexRecentlyAddedMediaUri);
        return ParsePlexMedia(await response.Content.ReadAsStringAsync());
    }

    private static IEnumerable<PlexMedia> ParsePlexMedia(string plexCatalog)
    {
        var plexCatalogJson = JsonDocument.Parse(plexCatalog);
        var root = plexCatalogJson.RootElement;
        var metadataJson = root.GetProperty("MediaContainer").GetProperty("Metadata").GetRawText();
        var plexMedias = JsonSerializer.Deserialize<List<PlexMedia>>(metadataJson);


        return plexMedias.Where(media => media.Type != MediaType.Unknown);
    }
}
