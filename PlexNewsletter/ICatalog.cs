namespace MediahubNewsletter;

public interface ICatalog
{
    Task<List<Media>> Medias();
}