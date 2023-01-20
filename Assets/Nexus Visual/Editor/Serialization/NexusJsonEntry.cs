using System;

namespace NexusVisual.Editor
{
    public struct NexusJsonEntry
    {
        public string id { get; }
        public Type type { get; }
        public string json { get; }

        public NexusJsonEntry(Type type, string id, string json)
        {
            this.id = id;
            this.type = type;
            this.json = json;
        }

        private bool Equals(NexusJsonEntry other)
        {
            return id == other.id && type == other.type && json == other.json;
        }

        public override bool Equals(object obj)
        {
            return obj is NexusJsonEntry other && Equals(other);
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