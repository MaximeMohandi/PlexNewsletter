using System.Text.Json;
using System.Text.Json.Serialization;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public class PlexMediaTypeJsonConverter : JsonConverter<MediaType>
{
    public override MediaType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string[] plexMovieTypes = { "movie" };
        string[] plexShowTypes = { "episode", "season", "show" };
        var plexMediaTypes = plexMovieTypes.Concat(plexShowTypes).ToArray();

        var type = reader.GetString()?.ToLower();
        if (type == string.Empty || !plexMediaTypes.Contains(type))
            return MediaType.Unknown;

        return plexShowTypes.Contains(type) ? MediaType.TvShow : MediaType.Movie;
    }

    public override void Write(Utf8JsonWriter writer, MediaType value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}