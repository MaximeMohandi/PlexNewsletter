using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Tests.MediaLibrary.Mocks;

public record FakeMedia : IMedia
{
    public string Title { get; init; }
    public MediaType Type { get; init; }
    public DateTime AddedAt { get; init; }
    public string Summary { get; init; }
    public int Season { get; init; }
    public string TvShow { get; init; }
    public int Episode { get; init; }

    public static IEnumerable<IMedia> GetMoviesAndShowsWithSomeFromToday()
    {
        return new FakeMedia[]
        {
            new()
            {
                Title = "Sharknado", Type = MediaType.Movie, AddedAt = DateTime.Today,
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new()
            {
                Title = "The Hobbit: The Battle of the Five Armies", Type = MediaType.Movie,
                AddedAt = DateTime.Today.AddDays(-1),
                Summary =
                    "After the Dragon leaves the Lonely Mountain, the people of Lake-town see a threat coming. Orcs, dwarves, elves and people prepare for war. Bilbo sees Thorin going mad and tries to help. Meanwhile, Gandalf is rescued from the Necromancer's prison and his rescuers realize who the Necromancer is."
            },
            new()
            {
                TvShow = "The Mandalorian", Season = 1, Episode = 2, Type = MediaType.TvShow, AddedAt = DateTime.Today,
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.", Title = "Chapter 1"
            }
        };
    }

    public static IEnumerable<IMedia> GetMoviesAndShowsWithNoneFromToday()
    {
        return new FakeMedia[]
        {
            new()
            {
                Title = "Sharknado", Type = MediaType.Movie, AddedAt = DateTime.Today.AddDays(-1),
                Summary =
                    "A freak hurricane hits Los Angeles, causing man-eating sharks to be scooped up in tornadoes and flooding the city with shark-infested seawater. Surfer and bar-owner Fin sets out with his friends Baz and Nova to rescue his estranged wife April and teenage daughter Claudia."
            },
            new()
            {
                Title = "The Hobbit: The Battle of the Five Armies", Type = MediaType.Movie,
                AddedAt = DateTime.Today.AddDays(-1),
                Summary =
                    "After the Dragon leaves the Lonely Mountain, the people of Lake-town see a threat coming. Orcs, dwarves, elves and people prepare for war. Bilbo sees Thorin going mad and tries to help. Meanwhile, Gandalf is rescued from the Necromancer's prison and his rescuers realize who the Necromancer is."
            },
            new()
            {
                TvShow = "The Mandalorian", Season = 1, Episode = 2, Type = MediaType.TvShow,
                AddedAt = DateTime.Today.AddDays(-1),
                Summary = "A Mandalorian bounty hunter tracks a target for a well-paying client.", Title = "Chapter 1"
            }
        };
    }
}