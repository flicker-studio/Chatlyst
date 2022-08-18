using System;

namespace AVG.Runtime.VisualGraph
{
    [Serializable]
    public class GraphNode
    {
        public string guid;
        public string characterName;
        public string dialogueText;
        public bool isStartNode;
        public bool isEndNode;


        public GraphNode()
        {
            guid = Guid.NewGuid().ToString();
        }
    }
}