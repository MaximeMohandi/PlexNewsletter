using FluentAssertions;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Newsletter.Discord;
using MediahubNewsletter.Tests.MediaLibrary.Mocks;

namespace MediahubNewsletter.Tests.Newsletter.Discord;

public class DiscordMessageWriterTests
{
    [Test]
    public void ShouldWriteMovieIntoDiscordEmbedMessage()
    {
        var expected = new DiscordMessage(new[]
        {
            new DiscordEmbedMessage("\ud83c\udfa5 Films :", new[]
            {
                new DiscordEmbedField("**Inception**", "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux."),
                new DiscordEmbedField("**Interstellar**", "Un groupe d'explorateurs utilise un trou de ver récemment découvert pour dépasser les limites des voyages spatiaux humains et conquérir les vastes distances d'un voyage interstellaire.")
            }),
        });

        var result = DiscordMessageWriter.Write(new[]
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
        });

        result.Should().BeEquivalentTo(expected);
    }

}
