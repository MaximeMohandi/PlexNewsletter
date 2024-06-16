using MediahubNewsletter.Catalog;

namespace MediahubNewsletter.MediaLibrary;

public class MediaLibrary
{
    private readonly ICatalog _catalog;

    public MediaLibrary(ICatalog catalog)
    {
        _catalog = catalog;
    }

    public async Task<List<Media>> RecentlyAddedMedia()
    {
        var medias = await _catalog.Medias();
        return medias.Where(MediaIsFromToday).ToList();
    }

    private static bool MediaIsFromToday(Media media) => media.AddedAt == DateTime.Today;
}
