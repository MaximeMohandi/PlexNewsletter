using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Newsletter.Discord;

public static class DiscordMessageWriter
{
    public static DiscordMessage Write(IEnumerable<IMedia> medias)
    {
        return new DiscordMessage(new List<DiscordEmbedMessage>
        {
            medias.WriteMovieToDiscord(),
            medias.WriteTvShowToDiscord()
        });
    }

    private static string FormatToSxxEyy(int number)
    {
        return number <= 9 ? $"0{number}" : number.ToString();
    }

    private static DiscordEmbedMessage WriteTvShowToDiscord(this IEnumerable<IMedia> medias)
    {
        return new DiscordEmbedMessage("**\ud83d\udcfa Séries :**", medias
            .Where(m => m.Type == MediaType.TvShow)
            .GroupBy(m => m.TvShow)
            .Select(shows => shows
                .GroupBy(m => m.Season)
                .Select(season =>
                {
                    var firstEpisode = season.First();
                    if (season.Count() > 1)
                    {
                        var lastEpisode = season.Last();
                        return new DiscordEmbedField(
                            $"**{firstEpisode.TvShow}** - S{FormatToSxxEyy(firstEpisode.Season)}E{FormatToSxxEyy(firstEpisode.Episode)}-{FormatToSxxEyy(lastEpisode.Episode)}",
                            firstEpisode.Summary);
                    }

                    return new DiscordEmbedField(
                        $"**{firstEpisode.TvShow}** - S{FormatToSxxEyy(firstEpisode.Season)}E{FormatToSxxEyy(firstEpisode.Episode)}",
                        firstEpisode.Summary);
                }))
            .SelectMany(x => x));
    }

    private static DiscordEmbedMessage WriteMovieToDiscord(this IEnumerable<IMedia> medias)
    {
        return new DiscordEmbedMessage("**\ud83c\udfa5 Films :**", medias
            .Where(m => m.Type == MediaType.Movie)
            .Select(m => new DiscordEmbedField($"**{m.Title}**", m.Summary)));
    }
}