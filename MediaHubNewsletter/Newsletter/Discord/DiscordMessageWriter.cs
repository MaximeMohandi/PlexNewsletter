using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Newsletter.Discord;

public static class DiscordMessageWriter
{
    public static DiscordMessage Write(IEnumerable<IMedia> media)
    {
        var movieFields = media
            .Where(m => m.Type == MediaType.Movie)
            .Select(m => new DiscordEmbedField($"**{m.Title}**", m.Summary));

        var tvShowFields = media
            .Where(m => m.Type == MediaType.TvShow);


        return new DiscordMessage(new List<DiscordEmbedMessage>
        {
            new("**\ud83c\udfa5 Films :**", movieFields),
            new("**\ud83d\udcfa Séries :**", AggregateFollowingEpisode(tvShowFields))
        });
    }

    private static string FormatToSxxEyy(int number) => number <= 9 ? $"0{number}" : number.ToString();

    private static IEnumerable<DiscordEmbedField> AggregateFollowingEpisode(IEnumerable<IMedia> medias)
    {
        var groupedTvShows = medias
            .GroupBy(m => m.TvShow)
            .Select(shows => shows
                .GroupBy(m => m.Season)
                .Select(season =>
                {
                   if(season.Count() > 1)
                   {
                       var firstEpisode = season.First();
                       var lastEpisode = season.Last();
                       return new DiscordEmbedField($"**{firstEpisode.TvShow}** - S{FormatToSxxEyy(firstEpisode.Season)}E{FormatToSxxEyy(firstEpisode.Episode)}-{FormatToSxxEyy(lastEpisode.Episode)}", firstEpisode.Summary);
                   }
                       var episode = season.First();
                       return new DiscordEmbedField($"**{episode.TvShow}** - S{FormatToSxxEyy(episode.Season)}E{FormatToSxxEyy(episode.Episode)}", episode.Summary);
                }));

        return groupedTvShows.SelectMany(x => x);
    }
}
