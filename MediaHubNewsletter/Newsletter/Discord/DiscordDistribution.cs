using System.Text;
using System.Text.Json;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Newsletter.Discord;

public class DiscordDistribution : IDistributionCanal
{
    private readonly HttpClient _client;
    private readonly string? _discordWebhook;
    private readonly IMediaLibrary _mediaLibrary;

    public DiscordDistribution(IMediaLibrary mediaLibrary, HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _mediaLibrary = mediaLibrary;
        _discordWebhook = configuration["DiscordClient:webhookUrl"];
    }

    public async Task Send()
    {
        var message = DiscordMessageWriter.Write(await _mediaLibrary.RecentlyAddedMedia());
        var jsonConfiguration = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true
        };
        var json = JsonSerializer.Serialize(message, jsonConfiguration);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        await _client.PostAsync(_discordWebhook, content);
    }
}
