namespace MediahubNewsletter;

public class MediaLibrary
{
    private readonly Catalog _catalog;

    public MediaLibrary(Catalog catalog)
    {
        _catalog = catalog;
    }

    public async Task<List<Media>> RecentlyAddedMedia()
    {
        return await _catalog.Medias();
    }

}
