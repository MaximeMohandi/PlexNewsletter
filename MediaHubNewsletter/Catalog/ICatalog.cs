using System.Collections;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public interface ICatalog
{
    Task<IEnumerable<IMedia>> Medias();
}
