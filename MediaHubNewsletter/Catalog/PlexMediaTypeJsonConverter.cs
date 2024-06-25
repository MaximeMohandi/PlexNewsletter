using System.Text.Json;
using System.Text.Json.Serialization;
using MediahubNewsletter.MediaLibrary;

namespace MediahubNewsletter.Catalog;

public class PlexMediaTypeJsonConverter: JsonConverter<MediaType>
{
    public override MediaType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString()?.ToLower().Equals("movie") is true ? MediaType.Movie : MediaType.TvShow;
    }

    public override void Write(Utf8JsonWriter writer, MediaType value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
