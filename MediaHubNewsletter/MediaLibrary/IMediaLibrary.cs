namespace MediahubNewsletter.MediaLibrary;

public interface IMediaLibrary
{
    Task<IEnumerable<IMedia>> RecentlyAddedMedia();
}