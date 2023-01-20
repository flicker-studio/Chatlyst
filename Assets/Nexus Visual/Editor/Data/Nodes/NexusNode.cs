using UnityEngine;

namespace NexusVisual.Editor
{
    public struct NexusNode
    {
        public string guid { get; }
        public string nextGuid { get; set; }
        public Rect nodePos { get; set; }

        public NexusNode(string guid, string nextGuid, Rect nodePos)
        {
            this.guid = guid;
            this.nextGuid = nextGuid;
            this.nodePos = nodePos;
        }
    }
}