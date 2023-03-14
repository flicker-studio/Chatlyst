using JetBrains.Annotations;
using Newtonsoft.Json;

namespace NexusVisual.Editor.Serialization
{
    internal readonly struct NexusJsonIndex
    {
        [JsonProperty] private readonly string _address;

        internal NexusJsonIndex([NotNull] string address)
        {
            _address = address;
        }
    }
}