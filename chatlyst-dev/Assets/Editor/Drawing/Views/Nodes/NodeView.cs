using System;
using System.Reflection;
using Chatlyst.Runtime.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor
{
    public interface INodeView
    {
    }
    public abstract class NodeView : Node, INodeView
    {
        protected NodeView(string uxmlPath)
        {
            var visualTree = Resources.Load<VisualTreeAsset>(uxmlPath);
            if (visualTree == null)
                throw new NullReferenceException($"Can't find the {visualTree}");
            visualTree.CloneTree(mainContainer);
            PortCreate();
        }

        private void PortCreate()
        {
            var portNumAttr = GetType().GetCustomAttribute<NodePortAttribute>();
            if (portNumAttr == null) throw new Exception($"{GetType().DeclaringType} doesn't have Port number Attribute!");
            for (int i = 0; i < portNumAttr.InputPortNum; i++)
            {
                var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
                inputContainer.Add(inputPort);
            }

            for (int i = 0; i < portNumAttr.OutputPortNum; i++)
            {
                var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
                outputContainer.Add(outputPort);
            }
        }

        public abstract void RefreshData();

        /// <summary>
        ///     Build a new node instance.
        /// </summary>
        /// <param name="pos">The position of the node.</param>
        public abstract void BuildNewInstance(Rect pos);

        /// <summary>
        ///     Rebuild a node instance from old data.
        /// </summary>
        /// <param name="nodeData">Old data.</param>
        public abstract void RebuildInstance(BasicNode nodeData);
    }
}
