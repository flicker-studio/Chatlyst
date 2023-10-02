using System;
using System.Reflection;
using Chatlyst.Editor.Attribute;
using Chatlyst.Runtime.Data;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor
{
    /// <summary>
    ///     Flag interface that can be displayed in graph.
    /// </summary>
    public interface INodeView
    {
    }
    /// <summary>
    /// </summary>
    public abstract class NodeView : Node, INodeView
    {
        /// <summary>
        ///     Use uxml to create the visual item.
        /// </summary>
        /// <param name="uxmlPath">Relative path of Uxml file.</param>
        /// <exception cref="NullReferenceException">The file in <paramref name="uxmlPath" /> is not exist.</exception>
        protected NodeView([NotNull]string uxmlPath)
        {
            var visualTree = Resources.Load<VisualTreeAsset>(uxmlPath);
            if (visualTree == null)
                throw new NullReferenceException($"Can't find the {visualTree}");
            visualTree.CloneTree(mainContainer);
            PortCreate();
        }

        /// <summary>
        ///     Ports will be automatically generated on the view.
        /// </summary>
        /// <remarks>
        ///     It can be configured using the <see cref="NodePortAttribute" />
        /// </remarks>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        ///     Synchronize the stored data with the current data
        /// </summary>
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
