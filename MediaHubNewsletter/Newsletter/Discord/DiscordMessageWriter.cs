using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Newsletter.Discord;

public class DiscordMessageWriter
{
    public static DiscordMessage Write(IEnumerable<IMedia> media)
    {
        var movieFields = media
            .Where(m => m.Type == MediaType.Movie)
            .Select(m => new DiscordEmbedField($"**{m.Title}**", m.Summary));

        var tvShowFields = media
            .Where(m => m.Type == MediaType.TvShow)
            .Select(m => new DiscordEmbedField($"**{m.TvShow}** - S{WriteEpisodeToStandard(m.Season)}E{WriteEpisodeToStandard(m.Episode)}", m.Summary));

        return new DiscordMessage(new List<DiscordEmbedMessage>
        {
            new("**\ud83c\udfa5 Films :**", movieFields),
            new("**\ud83d\udcfa Séries :**", tvShowFields)
        });
    }

    private static string WriteEpisodeToStandard(int episode)
    {
        return episode <= 9 ? $"0{episode}" : episode.ToString();
    }
}
