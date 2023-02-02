using System;
using Newtonsoft.Json;

namespace NexusVisual.Editor
{
    public struct NexusJsonEntity
    {
        public string id { get; }
        public Type type { get; }
        public string json { get; }
        public string userData { get; set; }

        public NexusJsonEntity(Type type, string id, string json)
        {
            this.id = id;
            this.type = type;
            this.json = json;
            userData = null;
        }

        public T ConvertToOrigin<T>()
        {
            if (typeof(T) != type)
            {
                throw new Exception("Mismatch type!");
            }

            return JsonConvert.DeserializeObject<T>(json);
        }

        private bool Equals(NexusJsonEntity other)
        {
            return id == other.id && type == other.type && json == other.json;
        }

        public override bool Equals(object obj)
        {
            return obj is NexusJsonEntity other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (id != null ? id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (type != null ? type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (json != null ? json.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(type)}: {type}, {nameof(json)}:\n{json}";
        }
    }
}