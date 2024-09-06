namespace MediahubNewsletter.NewsletterDistribution;

public record DiscordEmbedMessage(IEnumerable<DiscordEmbedField> MovieFields, IEnumerable<DiscordEmbedField> TvShowFields)
{
    string Title { get; } = "📢 **Nouveaux médias ajoutés !** 🎬📺";
    string Type { get; } = "rich";
    string Color { get; } = "#0099ff";
    string Timestamp { get; } = DateTime.Now.ToString("dd-MM-yyyy");
}
