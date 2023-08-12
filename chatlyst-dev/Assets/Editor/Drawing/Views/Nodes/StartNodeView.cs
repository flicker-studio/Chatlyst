using System;
using System.Numerics;
using Chatlyst.Runtime;
using UnityEngine;

namespace Chatlyst.Editor
{
    [SearchTreeName("开始节点")] [NodePort(0, 1)]
    public sealed class StartNodeView : NodeView, IVisible
    {
        private BeginNode _node;
        public string label
        {
            get => _node.StartLabel;
            set => _node.StartLabel = value;
        }

        public StartNodeView()
        {
            Construction("UXML/Start");
            Type = NodeType.BEG;
        }

        public void CreateNewInstance(Rect pos)
        {
            _node    = new BeginNode();
            userData = _node;
            SetPosition(pos);
        }

        public override void RefreshData()
        {
            _node.NodePos.X = GetPosition().position.x;
            _node.NodePos.Y = GetPosition().position.y;

            userData = _node;
        }

        public void RebuildInstance(BasicNode nodeData)
        {
            if (nodeData is not BeginNode data)
            {
                throw new Exception("Incorrect input！");
            }
            _node    = data;
            userData = _node;
            Type     = _node.NodeType;
            SetPosition(new Rect(data.NodePos.X, data.NodePos.Y, 1, 1));
        }
    }
}
