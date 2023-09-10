using System;
using Chatlyst.Runtime;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Chatlyst.Editor.Serialization
{
    public static class IndexJsonInternal
    {
        private static bool isSerializing   { get; set; }
        private static bool isDeserializing { get; set; }

        /// <summary>
        ///     Deserialize data from Json
        /// </summary>
        /// <param name="jsonText">Enter Json</param>
        /// <returns>Instance</returns>
        [CanBeNull]
        public static NodeIndex Deserialize(string jsonText)
        {
            var deserializeObject = new NodeIndex();
            if (isDeserializing)
            {
                throw new InvalidOperationException("Nested deserialization is not supported.");
            }

            try
            {
                deserializeObject = JsonConvert.DeserializeObject<NodeIndex>(jsonText);
                isDeserializing   = true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                isDeserializing = false;
            }

            return deserializeObject;
        }

        /// <summary>
        ///     Covert the node_index to json
        /// </summary>
        /// <param name="index">The node_index data</param>
        /// <returns>Json string</returns>
        /// <exception cref="InvalidOperationException">Type Error</exception>
        public static string Serialize(this NodeIndex index)
        {
            if (isSerializing)
            {
                throw new InvalidOperationException("Nested serialization is not supported.");
            }

            try
            {
                isSerializing = true;
                string jsonText = JsonConvert.SerializeObject(index, Formatting.Indented);
                return jsonText;
            }
            finally
            {
                isSerializing = false;
            }
        }
    }
}
