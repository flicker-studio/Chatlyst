using System;
using System.Linq;
using Newtonsoft.Json;

namespace Tests.Utility
{
    /// <summary>
    ///     Some of the utilities used within unit tests
    /// </summary>
    public static class Utilities
    {
        private static readonly Random Random = new Random();

        internal static JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings
                                                                           {
                                                                               Formatting = Formatting.Indented
                                                                           };

        /// <summary>
        ///     Get a random <see langword="int" />
        /// </summary>
        public static int RandomInt()
        {
            return Random.Next();
        }

        /// <summary>
        ///     Gets a random <see langword="string" />
        /// </summary>
        /// <param name="length">String length</param>
        public static string RandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(characters, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
