using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace NexusVisual.Editor.Serialization
{
    internal static class NexusJsonInternal
    {
        private static bool isSerializing { get; set; }
        private static bool isDeserializing { get; set; }

        [CanBeNull]
        public static IEnumerable<NexusJsonEntity> Deserialize(string jsonText)
        {
            var entries = new List<NexusJsonEntity>();
            if (isDeserializing)
            {
                throw new InvalidOperationException("Nested deserialization is not supported.");
            }

            try
            {
                isDeserializing = true;
                entries.AddRange(JsonConvert.DeserializeObject<List<NexusJsonEntity>>(jsonText));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                isDeserializing = false;
            }

            return entries;
        }

        public static string Serialize(List<NexusJsonEntity> entries)
        {
            if (isSerializing)
            {
                throw new InvalidOperationException("Nested serialization is not supported.");
            }

            try
            {
                isSerializing = true;
                var jsonText = JsonConvert.SerializeObject(entries, Formatting.Indented);
                return jsonText;
            }
            finally
            {
                isSerializing = false;
            }
        }
    }
}