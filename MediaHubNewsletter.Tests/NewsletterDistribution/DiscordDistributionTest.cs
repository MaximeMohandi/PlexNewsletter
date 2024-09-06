using FluentAssertions;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.NewsletterDistribution;
using MediahubNewsletter.Tests.MediaLibrary.Mocks;

namespace MediahubNewsletter.Tests.NewsletterDistribution;

public class DiscordDistributionTest
{
    [Test]
    public void ShouldConvertMediaToDiscordEmbedMessage()
    {
        var medias = new[]
        {
            new FakeMedia()
            {
                Title = "Inception",
                Type = MediaType.Movie,
                Summary =
                    "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux.",
            },
            new FakeMedia()
            {
                Title = "Interstellar",
                Type = MediaType.Movie,
                Summary =
                    "Un groupe d'explorateurs utilise un trou de ver récemment découvert pour dépasser les limites des voyages spatiaux humains et conquérir les vastes distances d'un voyage interstellaire.",

            },
            new FakeMedia()
            {
                Title = "The Mandalorian",
                Season = 1,
                Episode = 2,
                Type = MediaType.TvShow,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.",
                TvShow = "Chapter 1"
                }
        };

        var content = DiscordDistribution.ConvertToMessage(medias);

       content.Should().BeEquivalentTo(new DiscordEmbedMessage(new[]
        {
            new DiscordEmbedField("Inception", "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux."),
            new DiscordEmbedField("Interstellar", "Un groupe d'explorateurs utilise un trou de ver récemment découvert pour dépasser les limites des voyages spatiaux humains et conquérir les vastes distances d'un voyage interstellaire.")
        }, Enumerable.Empty<DiscordEmbedField>()));
    }

}
