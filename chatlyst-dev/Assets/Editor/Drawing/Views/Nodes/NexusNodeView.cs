using System;
using System.Reflection;
using Chatlyst.Editor.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor.Views
{
    public abstract class NexusNodeView : Node
    {
        public virtual NexusJsonEntity dataEntity { get; private set; }

        protected void Construction(string uxmlPath, NexusJsonEntity nodeEntity)
        {
            Visualization(uxmlPath);
            dataEntity = nodeEntity;
            PortCreate();
            ChatlystEditorWindow.GraphView.AddElement(this);
        }

        /// <summary>
        /// Get node uxml file
        /// </summary>
        private void Visualization(string uxmlPath)
        {
            var visualTree = Resources.Load<VisualTreeAsset>(uxmlPath);
            if (visualTree == null)
                throw new NullReferenceException($"Can't find the {visualTree}");
            visualTree.CloneTree(mainContainer);
        }

        public abstract void DataRefresh();
        
        private void PortCreate()
        {
            var portNumAttr = GetType().GetCustomAttribute<NodePortAttribute>();
            if (portNumAttr == null) throw new Exception($"{GetType().DeclaringType} doesn't have Port number Attribute!");
            for (var i = 0; i < portNumAttr.InputPortNum; i++)
            {
                var inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
                inputContainer.Add(inputPort);
            }

            for (var i = 0; i < portNumAttr.OutputPortNum; i++)
            {
                var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
                outputContainer.Add(outputPort);
            }
        }
    }
}