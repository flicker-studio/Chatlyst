using System.Numerics;
using Newtonsoft.Json;

namespace Chatlyst.Runtime
{
    public abstract class Node
    {
    };
    public abstract class BasicNode<T> : Node
    {
        public string Guid;
        public NodeType NodeType;
        public string NextGuid;
        public Vector2 NodePos;
        public T NodeData;
        private readonly string _tag;

        protected BasicNode()
        {
            Guid = System.Guid.NewGuid().ToString();
            NodeType = 0;
            NextGuid = null;
            NodePos = Vector2.Zero;

            _tag = System.Guid.NewGuid().ToString();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public abstract BaseNode ToBaseNode();
        private bool Equals(BasicNode<T> other) => Guid == other.Guid;

        public override bool Equals(object obj) => obj is BasicNode<T> other && Equals(other);

        public override int GetHashCode() => _tag.GetHashCode();
    }
}
