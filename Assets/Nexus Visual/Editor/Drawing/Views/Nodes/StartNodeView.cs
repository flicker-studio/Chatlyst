using JetBrains.Annotations;
using NexusVisual.Editor.Data;
using UnityEngine;

namespace NexusVisual.Editor.Views
{
    [SearchTreeName("开始节点"), NodePort(0, 1)]
    public sealed class StartNodeView : NexusNodeView, IVisible
    {
        private const string UxmlPath = "UXML/Start";
        public override NexusJsonEntity dataEntity
        {
            get
            {
                var entity = _node.ConvertToEntity();
                entity.userData = GetType().FullName;
                return entity;
            }
        }
        [NotNull] private StartNode _node;

        public StartNodeView()
        {
            _node = new StartNode("Default");
        }

        public void CreateInstance(Rect pos)
        {
            SetPosition(pos);
            DataRefresh();
            var entity = _node.ConvertToEntity();
            entity.userData = GetType().FullName;
            Construction(UxmlPath, entity);
        }

        public void RebuildInstance(NexusJsonEntity entity)
        {
            _node = entity.ConvertToOrigin<StartNode>();
            Construction(UxmlPath, entity);
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