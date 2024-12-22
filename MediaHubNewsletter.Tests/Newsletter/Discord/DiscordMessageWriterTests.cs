using FluentAssertions;
using MediahubNewsletter.MediaLibrary;
using MediahubNewsletter.Newsletter.Discord;
using MediahubNewsletter.Tests.MediaLibrary.Mocks;

namespace MediahubNewsletter.Tests.Newsletter.Discord;

public class DiscordMessageWriterTests
{
    [Test]
    public void ShouldWriteIntoDiscordEmbedMessage()
    {
        var expected = new DiscordMessage(new[]
        {
            new DiscordEmbedMessage("**\ud83c\udfa5 Films :**", new[]
            {
                new DiscordEmbedField("**Inception**",
                    "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux."),
                new DiscordEmbedField("**Interstellar**",
                    "Un groupe d'explorateurs utilise un trou de ver récemment découvert pour dépasser les limites des voyages spatiaux humains et conquérir les vastes distances d'un voyage interstellaire.")
            }),
            new DiscordEmbedMessage("**\ud83d\udcfa Séries :**", new[]
            {
                new DiscordEmbedField("**Ring Fit Adventure** - S01E01",
                    "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux."),
                new DiscordEmbedField("**The Mandalorian** - S01E02-04",
                    "A Mandalorian bounty hunter tracks a target for a well-paying client."),
                new DiscordEmbedField("**The Mandalorian** - S02E03",
                    "The battered Mandalorian returns to his client for his reward.")
            })
        });

        var result = DiscordMessageWriter.Write(new[]
        {
            new FakeMedia
            {
                Title = "Inception",
                Type = MediaType.Movie,
                Summary =
                    "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux."
            },

            new FakeMedia
            {
                Title = "Interstellar",
                Type = MediaType.Movie,
                Summary =
                    "Un groupe d'explorateurs utilise un trou de ver récemment découvert pour dépasser les limites des voyages spatiaux humains et conquérir les vastes distances d'un voyage interstellaire."
            },
            new FakeMedia
            {
                Title = "Ring Fit Adventure",
                Season = 1,
                Episode = 1,
                Type = MediaType.TvShow,
                Summary =
                    "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux.",
                TvShow = "Ring Fit Adventure"
            },
            new FakeMedia
            {
                Title = "Chapter 1",
                Season = 1,
                Episode = 2,
                Type = MediaType.TvShow,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.",
                TvShow = "The Mandalorian"
            },
            new FakeMedia
            {
                Title = "Chapter 2",
                Season = 1,
                Episode = 3,
                Type = MediaType.TvShow,
                Summary = "The battered Mandalorian returns to his client for his reward.",
                TvShow = "The Mandalorian"
            },
            new FakeMedia
            {
                Title = "Chapter 3",
                Season = 1,
                Episode = 4,
                Type = MediaType.TvShow,
                Summary = "The Mandalorian teams up with an ex-soldier to protect a village from raiders.",
                TvShow = "The Mandalorian"
            },
            new FakeMedia
            {
                Title = "Chapter 4",
                Season = 2,
                Episode = 3,
                Type = MediaType.TvShow,
                Summary = "The battered Mandalorian returns to his client for his reward.",
                TvShow = "The Mandalorian"
            }
        });

        result.Should().BeEquivalentTo(expected);
    }
}