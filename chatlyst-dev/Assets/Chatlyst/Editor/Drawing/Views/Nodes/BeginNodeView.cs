using System;
using Chatlyst.Editor.Attribute;
using Chatlyst.Runtime;
using Chatlyst.Runtime.Data;
using UnityEngine;

namespace Chatlyst.Editor
{
    /// <summary>
    ///     Begin Node View
    /// </summary>
    [SearchTreeName("开始节点")] [NodePort(0, 1)]
    public sealed class BeginNodeView : NodeView
    {
        /// <inheritdoc />
        public BeginNodeView() : base("UXML/Start") { }

        /// <inheritdoc />
        public override void BuildNewInstance(Rect pos)
        {
            var node = new BeginNode();
            SetPosition(pos);
            node.SetRect(pos);
            userData = node;
        }

        /// <inheritdoc />
        /// <exception cref="Exception">Type of data entered is incorrect</exception>
        public override void RebuildInstance(BasicNode nodeData)
        {
            if (nodeData is not BeginNode data)
                throw new Exception("Incorrect input！");
            userData = data;
            SetPosition(data.GetRect());
        }

        /// <inheritdoc />
        /// <exception cref="Exception">Refresh data error.</exception>
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
