using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Newsletter.Discord;

public class DiscordMessageWriter
{
    public static DiscordMessage Write(IEnumerable<IMedia> media)
    {
        var movieFields = media
            .Where(m => m.Type == MediaType.Movie)
            .Select(m => new DiscordEmbedField($"**{m.Title}**", m.Summary));

        return new DiscordMessage(new List<DiscordEmbedMessage>
        {
            new("\ud83c\udfa5 Films :", movieFields)
        });
    }
}
