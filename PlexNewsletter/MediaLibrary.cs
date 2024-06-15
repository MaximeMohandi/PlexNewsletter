using System.Net.Http.Headers;
using System.Text.Json;

namespace MediahubNewsletter;

public class MediaLibrary
{
    private const string PlexUrl = "http://localhost:32400";
    private const string PlexToken = "token";

    public static async Task<List<Media>> RecentlyAddedMedia(HttpClient client)
    {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var plexRecentlyAddedMediaUri = new Uri($"{PlexUrl}/library/recentlyAdded?X-Plex-Token={PlexToken}");
        var response = client.GetAsync(plexRecentlyAddedMediaUri).Result;
        var content = response.Content.ReadAsStringAsync().Result;

        var mediaList = new List<Media>();
        using(JsonDocument doc = JsonDocument.Parse(content))
        {
            var root = doc.RootElement;
            var media = root.GetProperty("MediaContainer").GetProperty("Metadata");

            foreach (var item in media.EnumerateArray())
            {
                var title = item.GetProperty("title").GetString();
                var type = item.GetProperty("type").GetString();
                var addedAt = item.GetProperty("addedAt").GetUInt32();
                var summary = item.GetProperty("summary").GetString();
                mediaList.Add(new Media { Title = title, type = type, AddedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(addedAt), Summary = summary});
            }


        }
        return mediaList;
    }
}
