using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Chatlyst.Editor.Serialization
{
    public static class NexusJsonUtility
    {
        public static readonly JsonSerializerSettings IgnoreLoopSetting = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        [JsonObject(MemberSerialization.OptIn)]
        private struct SaveStruct
        {
            [JsonProperty] private List<NexusJsonIndex> _indices;
            [JsonProperty] private List<NexusJsonEntity> _entities;
            public List<NexusJsonEntity> entities => _entities;
            public List<NexusJsonIndex> indices => _indices;

            public void Add(object obj)
            {
                var entity = new NexusJsonEntity(obj, out var index);
                _indices.Add(index);
                _entities.Add(entity);
            }

            public void AddRange(IEnumerable<object> objs)
            {
                foreach (var obj in objs)
                {
                    Add(obj);
                }
            }
        }


        public static string SerializeIEnumerable(IEnumerable<object> objects, JsonSerializerSettings settings = null)
        {
            var a = new SaveStruct();
            a.AddRange(objects);
            return JsonConvert.SerializeObject(a, Formatting.Indented, settings == null ? IgnoreLoopSetting : null);
        }

        public static IEnumerable<T> DeserializeIEnumerable<T>(string text)
        {
            var revert = JsonConvert.DeserializeObject<SaveStruct>(text);
            return revert.entities.Select(entity => entity.Recover<T>());
        }
    }
}