using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Chatlyst.Runtime
{
    [Obsolete]
    public class BaseNode
    {
        public Type   NodeType;
        public string NodeJson;

        public bool TryToSource<T>([CanBeNull] out T ans) where T : BasicNode
        {
            ans = null;
            if (typeof(T) == NodeType)
            {
                ans = JsonConvert.DeserializeObject<T>(NodeJson);
            }
            return ans != null;
        }

        private bool Equals(BaseNode other) => NodeType == other.NodeType && NodeJson == other.NodeJson;

        public override bool Equals(object obj) => obj is BaseNode other && Equals(other);

        public override int GetHashCode() => NodeJson.GetHashCode();
    }
}
