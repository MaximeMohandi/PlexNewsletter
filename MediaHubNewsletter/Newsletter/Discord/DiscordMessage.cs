namespace MediahubNewsletter.Newsletter.Discord;

public record DiscordMessage(IEnumerable<DiscordEmbedMessage> Embeds)
{
    private string Content { get; } = "📢 **Nouveaux médias ajoutés !** 🎬📺";
    private string Tts { get; } = "false";
    private string Username { get; } = "MediaHub Newsletter";
}
