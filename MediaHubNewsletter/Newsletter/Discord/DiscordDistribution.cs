using System.Text;
using System.Text.Json;
using MediahubNewsletter.MediaLibrary;
using PlexNewsletter.Newsletter;

namespace MediahubNewsletter.Newsletter.Discord;

public class DiscordDistribution : IDistributionCanal
{
   private readonly IMediaLibrary _mediaLibrary;
   private readonly string? _discordWebhook;

   public DiscordDistribution(IMediaLibrary mediaLibrary, IConfiguration configuration)
   {
        _mediaLibrary = mediaLibrary;
        _discordWebhook = configuration["DiscordClient:webhookUrl"];
   }

   public async Task Send()
   {
       var message = DiscordMessageWriter.Write(await _mediaLibrary.RecentlyAddedMedia());
       var json = JsonSerializer.Serialize(message);
       var content = new StringContent(json, Encoding.UTF8, "application/json");
        using var _httpClient = new HttpClient();
       await _httpClient.PostAsync(_discordWebhook, content);
   }
}
