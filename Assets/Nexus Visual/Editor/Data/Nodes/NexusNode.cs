using Newtonsoft.Json;
using UnityEngine;

namespace NexusVisual.Editor.Data
{
    public class NexusNode
    {
        [JsonProperty]
        public readonly string Guid;
        public string NextGuid;
        public Rect NodePos;

        protected NexusNode()
        {
            Guid = System.Guid.NewGuid().ToString();
            NextGuid = null;
            NodePos = new Rect();
        }

        private bool Equals(NexusNode other) =>
            Guid == other.Guid && NextGuid == other.NextGuid && NodePos == other.NodePos;

        public override bool Equals(object obj) =>
            obj is NexusNode other && Equals(other);

        public override int GetHashCode() =>
            Guid.GetHashCode();
    }
}