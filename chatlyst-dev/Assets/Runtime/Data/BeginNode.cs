namespace Chatlyst.Runtime
{
    public class BeginNode : BasicNode
    {

        public string StartLabel;
        public int Number;

        public BeginNode(string label, int number) : base()
        {
            NodeType = NodeType.BEG;
            StartLabel = label;
            Number = number;
        }

        public override BaseNode ToBaseNode() => new BaseNode
        {
            NodeType = typeof(BeginNode),
            NodeJson = ToJson()
        };
    }
}
