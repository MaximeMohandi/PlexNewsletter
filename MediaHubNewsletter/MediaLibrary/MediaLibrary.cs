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
        return await _catalog.Medias();
    }

}
