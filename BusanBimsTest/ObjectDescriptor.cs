
using BusanBimsLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusanBimsTest
{
    internal static class ObjectDescriptor
    {
        internal static string Describe(this object? obj)
        {
            if (obj == null)
            {
                return "N/A";
            }
            else
            {
                JsonSerializerOptions options = new()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    WriteIndented = true,
                };
                options.Converters.Add(new GeolocationJsonConverter());
                return Regex.Unescape(JsonSerializer.Serialize(obj, obj.GetType(), options));
            }
        }
    }

    internal class GeolocationJsonConverter : JsonConverter<Geolocation>
    {
        public override Geolocation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Geolocation value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
