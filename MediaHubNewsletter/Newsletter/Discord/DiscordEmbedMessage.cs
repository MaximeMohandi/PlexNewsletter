namespace MediahubNewsletter.Newsletter.Discord;

public record DiscordEmbedMessage(string Title, IEnumerable<DiscordEmbedField> EmbedFields)
{
    string Type { get; } = "rich";
    string Color { get; } = "#0099ff";
    string Timestamp { get; } = DateTime.Now.ToString("dd-MM-yyyy");
}
