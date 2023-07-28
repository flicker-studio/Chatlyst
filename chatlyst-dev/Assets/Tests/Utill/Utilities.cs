using System.Linq;
using Newtonsoft.Json;
using Random = System.Random;

public static class Utilities
{
    private static readonly Random Random = new Random();

    internal static JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
    };
    public static string RandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(characters, length).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}
