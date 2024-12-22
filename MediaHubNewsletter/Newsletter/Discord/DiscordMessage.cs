namespace MediahubNewsletter.Newsletter.Discord;

public record DiscordMessage(IEnumerable<DiscordEmbedMessage> Embeds)
{
    public string Content { get; init; } = "📢 **Nouveaux médias ajoutés !** 🎬📺";
    public string Tts { get; init; } = "false";
    public string Username { get; init; } = "MediaHub Newsletter";
}