using MediahubNewsletter.Catalog;

namespace MediahubNewsletter.MediaLibrary;

public class MediaLibrary : IMediaLibrary
{
    private readonly ICatalog _catalog;

    public MediaLibrary(ICatalog catalog)
    {
        _catalog = catalog;
    }

    public async Task<IEnumerable<IMedia>> RecentlyAddedMedia()
    {
        var medias = await _catalog.Medias();
        return medias.Where(MediaIsFromToday);
    }

    private static bool MediaIsFromToday(IMedia media) => media.AddedAt == DateTime.Today;
}
