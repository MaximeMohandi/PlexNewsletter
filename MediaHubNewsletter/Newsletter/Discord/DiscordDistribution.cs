using System.Text;
using System.Text.Json;
using MediahubNewsletter.MediaLibrary;
using PlexNewsletter.Newsletter;

namespace MediahubNewsletter.Newsletter.Discord;

public class DiscordDistribution : IDistributionCanal
{
   private readonly IMediaLibrary _mediaLibrary;
   private readonly string? _discordWebhook;
   private readonly HttpClient _client;

   public DiscordDistribution(IMediaLibrary mediaLibrary, HttpClient client, IConfiguration configuration)
   {
        _client = client;
        _mediaLibrary = mediaLibrary;
        _discordWebhook = configuration["DiscordClient:webhookUrl"];
   }

   public async Task Send()
   {
       var message = DiscordMessageWriter.Write(await _mediaLibrary.RecentlyAddedMedia());
       var json = JsonSerializer.Serialize(message);
       var content = new StringContent(json, Encoding.UTF8, "application/json");
       await _client.PostAsync(_discordWebhook, content);
   }
}
