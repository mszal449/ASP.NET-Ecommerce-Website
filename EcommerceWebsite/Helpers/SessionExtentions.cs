using System.Text.Json;
using System.Text.Json.Serialization;

namespace EcommerceWebsite.Helpers;

public static class SessionExtentions
{
    // Save object as JSON to session
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));    }

    // Retrieve object from JSON in session
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);    }
}