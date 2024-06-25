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

    public async Task<IEnumerable<IMedia>> Medias()
    {
        var plexRecentlyAddedMediaUri = new Uri($"{PlexUrl}/library/recentlyAdded?X-Plex-Token={PlexToken}");
        var response = await _client.GetAsync(plexRecentlyAddedMediaUri);
        return ParsePlexMedia(await response.Content.ReadAsStringAsync());
    }

    private static IEnumerable<PlexMedia> ParsePlexMedia(string plexCatalog)
    {
        var plexCatalogJson = JsonDocument.Parse(plexCatalog);
        var root = plexCatalogJson.RootElement;
        var metadataJson = root.GetProperty("MediaContainer").GetProperty("Metadata").GetRawText();
        var plexMedias = JsonSerializer.Deserialize<List<PlexMedia>>(metadataJson);



        return plexMedias.Select(plexMedia => new PlexMedia()
        {
            Title = plexMedia.Title,
            Type = plexMedia.Type,
            AddedAt = plexMedia.AddedAt,
            Summary = plexMedia.Summary,
            Season = plexMedia.Season,
            TvShow = plexMedia.TvShow,
            Episode = plexMedia.Episode
        }).Where(media => media.Type != MediaType.Unknown);
    }
}
