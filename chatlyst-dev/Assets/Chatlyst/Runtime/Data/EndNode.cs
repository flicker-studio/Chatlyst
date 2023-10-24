using Chatlyst.Runtime.Data;

namespace Chatlyst.Runtime
{
    public class EndNode : BasicNode
    {
        public string StartLabel;
        public int    Number;

        public EndNode(string label = "Default End Label", int number = -1)
        {
            NodeType   = NodeType.END;
            StartLabel = label;
            Number     = number;
        }
    }
}
