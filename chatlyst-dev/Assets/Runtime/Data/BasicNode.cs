using Newtonsoft.Json;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Chatlyst.Runtime
{
    public abstract class BasicNode
    {
        [JsonProperty]
        public readonly string Guid;
        public NodeType NodeType;
        public string NextGuid;
        public Vector2 NodePos;

        protected BasicNode()
        {
            Guid = System.Guid.NewGuid().ToString();
            NodeType = 0;
            NextGuid = null;
            NodePos = Vector2.Zero;
        }

        public void StoresLocation(Rect rect)
        {
            NodePos.X = rect.position.x;
            NodePos.Y = rect.position.y;
        }

        public string ToJson() => JsonConvert.SerializeObject(this);

        private bool Equals(BasicNode other) => Guid == other.Guid;

        public override bool Equals(object obj) => obj is BasicNode other && Equals(other);

        public override int GetHashCode() => Guid.GetHashCode();
    }
}
