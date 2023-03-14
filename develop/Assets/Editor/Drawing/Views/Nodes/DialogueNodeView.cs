using NexusVisual.Editor.Data;
using NexusVisual.Editor.Serialization;
using UnityEngine;

namespace NexusVisual.Editor.Views
{
    [SearchTreeName("对话节点"), NodePort(1, 1)]
    public class DialogueNodeView : NexusNodeView, IVisible
    {
        private const string UxmlPath = "UXML/Start";
        public override NexusJsonEntity dataEntity
        {
            get
            {
                var entity = _node.ConvertToEntity();
                // entity.userData = GetType().FullName;
                return entity;
            }
        }
        public DialoguesNode _node;

        public override void DataRefresh()
        {
            _node.NodePos = GetPosition();
        }

        public void CreateInstance(Rect pos)
        {
            _node = new DialoguesNode();
            SetPosition(pos);
            DataRefresh();
            var entity = _node.ConvertToEntity();
            //  entity.userData = GetType().FullName;
            Construction(UxmlPath, entity);
        }

        public void RebuildInstance(NexusJsonEntity entity)
        {
            _node = entity.ConvertToOrigin<DialoguesNode>();
            Construction(UxmlPath, entity);
            SetPosition(_node.NodePos);
        }
    }
}