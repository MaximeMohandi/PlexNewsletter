using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.NewsletterDistribution;

public class DiscordDistribution
{
    public static DiscordEmbedMessage ConvertToMessage(IEnumerable<IMedia> media)
    {
        var movieFields = media
            .Where(m => m.Type == MediaType.Movie)
            .Select(m => new DiscordEmbedField(m.Title, m.Summary));

        return new DiscordEmbedMessage(movieFields, Enumerable.Empty<DiscordEmbedField>());
    }
}
