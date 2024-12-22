namespace MediahubNewsletter.Newsletter.Discord;

public record DiscordEmbedMessage(string Title, IEnumerable<DiscordEmbedField> EmbedFields)
{
    private string Type { get; } = "rich";
    private string Color { get; } = "#0099ff";
    private string Timestamp { get; } = DateTime.Now.ToString("dd-MM-yyyy");
}