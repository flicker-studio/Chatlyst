using System;
using Chatlyst.Editor.Attribute;
using Chatlyst.Runtime;
using Chatlyst.Runtime.Data;
using UnityEngine;

namespace Chatlyst.Editor
{
    /// <summary>
    ///     End Node View
    /// </summary>
    [SearchTreeName("结束节点")] [NodePort(1, 0)]
    public class EndNodeView : NodeView
    {
        /// <inheritdoc />
        public EndNodeView() : base("UXML/Start") { }

        /// <inheritdoc />
        public override void BuildNewInstance(Rect pos)
        {
            var node = new EndNode();
            SetPosition(pos);
            node.SetRect(pos);
            userData = node;
        }

        /// <inheritdoc />
        /// <exception cref="Exception">Type of data entered is incorrect</exception>
        public override void RebuildInstance(BasicNode nodeData)
        {
            if (nodeData is not EndNode data)
                throw new Exception("Incorrect input！");
            userData = data;
            SetPosition(data.GetRect());
        }

        /// <inheritdoc />
        /// <exception cref="Exception">Refresh data error.</exception>
        public override void RefreshData()
        {
            if (userData is EndNode node)
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
