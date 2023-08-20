using Chatlyst.Runtime.Data;

namespace Chatlyst.Runtime
{
    public class BeginNode : BasicNode
    {
        public string StartLabel;
        public int    Number;

        public BeginNode(string label = "Default Label", int number = -1)
        {
            NodeType   = NodeType.BEG;
            StartLabel = label;
            Number     = number;
        }
    }
}
