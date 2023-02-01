using JetBrains.Annotations;
using UnityEngine;

namespace NexusVisual.Editor.Views
{
    [SearchTreeName("开始节点"), NodePort(0, 1)]
    public sealed class StartNodeView : NexusNodeView, IVisible
    {
        private const string UxmlPath = "UXML/Start";
        [NotNull] private Data.StartNode _node;

        public StartNodeView(Rect pos)
        {
            _node = new Data.StartNode("StartTest");
            Construction(UxmlPath, _node.ConvertToEntry());
            SetPosition(pos);
        }

        public StartNodeView(NexusJsonEntry entry)
        {
            _node = entry.ConvertToOrigin<Data.StartNode>();
            Construction(UxmlPath, entry);
            SetPosition(_node.NodePos);
        }

        public override void DataRefresh()
        {
            _node.NodePos = GetPosition();
        }

        private protected override void DataBind()
        {
        }
    }
}