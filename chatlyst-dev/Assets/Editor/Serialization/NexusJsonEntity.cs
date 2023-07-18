using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatlyst.Editor.Serialization
{
    public readonly struct NexusJsonEntity
    {
        [JsonProperty] private readonly JObject _entity;
        [JsonProperty] private readonly string _index;
        [JsonProperty] private readonly string _dataType;

        internal NexusJsonEntity([NotNull] object target, out NexusJsonIndex index)
        {
            _entity = JObject.FromObject(target);
            _index = Guid.NewGuid().ToString();
            _dataType = target.GetType().ToString();
            index = new NexusJsonIndex(_index);
        }

        internal T Recover<T>() => _entity.ToObject<T>();
        
        public T ConvertToOrigin<T>()
        {
            throw new Exception("badass");
        }
    }
}