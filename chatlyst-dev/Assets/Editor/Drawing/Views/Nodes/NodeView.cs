using System;
using System.Reflection;
using Chatlyst.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor
{
    public abstract class NodeView : Node
    {
        protected NodeType  Type;
        public    BasicNode Data;

        protected void Construction(string uxmlPath)
        {
            Visualization(uxmlPath);
            PortCreate();
        }

        /// <summary>
        ///     Get node uxml file
        /// </summary>
        private void Visualization(string uxmlPath)
        {
            var visualTree = Resources.Load<VisualTreeAsset>(uxmlPath);
            if (visualTree == null)
                throw new NullReferenceException($"Can't find the {visualTree}");
            visualTree.CloneTree(mainContainer);
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
    }
}
