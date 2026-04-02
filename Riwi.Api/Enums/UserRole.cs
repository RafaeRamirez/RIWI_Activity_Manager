using System.Text.Json.Serialization;

namespace Riwi.Api.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin,
        Coder
    }
}
