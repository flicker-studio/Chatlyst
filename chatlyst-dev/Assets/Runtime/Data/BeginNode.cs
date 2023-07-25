namespace Chatlyst.Runtime
{
    public class BeginNode : BasicNode<string>
    {
        public string startLabel
        {
            get => NodeData;
            private set => NodeData = value;
        }

        public BeginNode(string label) : base()
        {
            NodeType = NodeType.BEG;
            startLabel = label;
        }
        
        public override BaseNode ToBaseNode() => new BaseNode
        {
            NodeType = typeof(BeginNode),
            NodeJson = ToJson()
        };
    }
}
