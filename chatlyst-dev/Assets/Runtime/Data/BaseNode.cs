using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
namespace Chatlyst.Runtime
{
    [Serializable]
    public class BaseNode
    {
        public Type NodeType;
        public string NodeJson;

        [CanBeNull]
        public bool TryToSource<T>(out T ans) where T : Node
        {
            if (typeof(T) == NodeType)
            {
                ans = JsonConvert.DeserializeObject<T>(NodeJson);
                return true;
            }
            ans = null;
            return false;
        }

        private bool Equals(BaseNode other) => NodeType == other.NodeType && NodeJson == other.NodeJson;

        public override bool Equals(object obj) => obj is BaseNode other && Equals(other);

        public override int GetHashCode() => NodeJson.GetHashCode();
    }
}
