using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace NexusVisual.Editor
{
    internal static class NexusJsonInternal
    {
        public static List<NexusJsonEntry> Entries;

        public static bool IsSerializing;
        public static bool IsDeserializing;

        public static void Deserialize(String jsonText)
        {
            var entries = new List<NexusJsonEntry>();
            if (IsDeserializing)
            {
                throw new InvalidOperationException("Nested deserialization is not supported.");
            }

            try
            {
                IsDeserializing = true;
                entries.AddRange(JsonConvert.DeserializeObject<List<NexusJsonEntry>>(jsonText));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                Entries = entries;
                IsDeserializing = false;
            }
        }

        public static string Serialize(List<NexusJsonEntry> entries)
        {
            if (IsSerializing)
            {
                throw new InvalidOperationException("Nested serialization is not supported.");
            }

            try
            {
                IsSerializing = true;
                var jsonText = JsonConvert.SerializeObject(entries);
                return jsonText;
            }
            finally
            {
                IsSerializing = false;
                Entries = entries;
            }
        }
    }
}