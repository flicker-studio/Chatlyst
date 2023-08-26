using System;
using Chatlyst.Runtime;
using Chatlyst.Runtime.Data;
using UnityEngine;

namespace Chatlyst.Editor
{
    [SearchTreeName("开始节点")] [NodePort(0, 1)]
    public sealed class BeginNodeView : NodeView
    {
        public BeginNodeView() : base("UXML/Start") { }

        public override void BuildNewInstance(Rect pos)
        {
            var node = new BeginNode();
            SetPosition(pos);
            node.SetRect(pos);
            userData = node;
        }

        public override void RebuildInstance(BasicNode nodeData)
        {
            if (nodeData is not BeginNode data)
                throw new Exception("Incorrect input！");
            userData = data;
            SetPosition(data.GetRect());
        }

        public override void RefreshData()
        {
            if (userData is BeginNode node)
            {
                node.NodePos.X = GetPosition().position.x;
                node.NodePos.Y = GetPosition().position.y;
            }
            else
            {
                throw new Exception("Unboxing Error!");
            }
            userData = node;
        }
    }
}
