using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public interface ICatalog
{
    Task<List<Media>> Medias();
}