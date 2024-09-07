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
            new DiscordEmbedMessage("**\ud83c\udfa5 Films :**", new[]
            {
                new DiscordEmbedField("**Inception**", "Un voleur qui s'infiltre dans les rêves de ses cibles pour voler leurs secrets les plus précieux."),
                new DiscordEmbedField("**Interstellar**", "Un groupe d'explorateurs utilise un trou de ver récemment découvert pour dépasser les limites des voyages spatiaux humains et conquérir les vastes distances d'un voyage interstellaire.")
            }),
            new DiscordEmbedMessage("**\ud83d\udcfa Séries :**", Enumerable.Empty<DiscordEmbedField>())
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

    [Test]
    public void ShouldWriteSingleEpisodeToDiscordEmbedmessage()
    {
        var expected = new DiscordMessage(new[]
        {
            new DiscordEmbedMessage("**\ud83d\udcfa Séries :**", new[]
            {
                new DiscordEmbedField("**The Mandalorian** - S01E02", "A Mandalorian bounty hunter tracks a target for a well-paying client.")
            }),
            new DiscordEmbedMessage("**\ud83c\udfa5 Films :**", Enumerable.Empty<DiscordEmbedField>())
        });

        var result = DiscordMessageWriter.Write(new[]
        {
            new FakeMedia()
            {
                Title = "Chapter 1",
                Season = 1,
                Episode = 2,
                Type = MediaType.TvShow,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.",
                TvShow = "The Mandalorian"
            }
        });

        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void ShouldAggregateWhenEpisodeFollows()
    {
       var expected = new DiscordMessage(new[]
       {
           new DiscordEmbedMessage("**\ud83d\udcfa Séries :**", new[]
           {
               new DiscordEmbedField("**The Mandalorian** - S01E02-04", "A Mandalorian bounty hunter tracks a target for a well-paying client."),
           }),
           new DiscordEmbedMessage("**\ud83c\udfa5 Films :**", Enumerable.Empty<DiscordEmbedField>())
       });

       var result = DiscordMessageWriter.Write(new[]
       {
           new FakeMedia()
           {
               Title = "Chapter 1",
               Season = 1,
               Episode = 2,
               Type = MediaType.TvShow,
               Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.",
               TvShow = "The Mandalorian"
           },
           new FakeMedia()
           {
               Title = "Chapter 2",
               Season = 1,
               Episode = 3,
               Type = MediaType.TvShow,
               Summary = "The battered Mandalorian returns to his client for his reward.",
               TvShow = "The Mandalorian"
           },
           new FakeMedia()
           {
               Title = "Chapter 3",
               Season = 1,
               Episode = 4,
               Type = MediaType.TvShow,
               Summary = "The Mandalorian teams up with an ex-soldier to protect a village from raiders.",
               TvShow = "The Mandalorian"
           }
       });

       result.Should().BeEquivalentTo(expected);
    }
}
