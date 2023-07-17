using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Chatlyst.Editor.Serialization
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