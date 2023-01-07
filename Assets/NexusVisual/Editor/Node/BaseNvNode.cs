using System;
using JetBrains.Annotations;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    ///<summary>
    /// The node needed to be visualization
    /// </summary>
    internal interface IVisible
    {
    }

    /// <summary>
    /// Nv base class
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public abstract class BaseNvNode<T> : Node
        where T : BaseData
    {
        protected readonly VisualElement mainElement = new VisualElement();
        protected VisualTreeAsset visualTree;
        public SerializedObject serializedObject;

        protected virtual void Construction([CanBeNull] T nodeData, Rect targetPos)
        {
            Visualization();
            //if node data is null means need creat a new node
            //else we need copy the old 
            if (nodeData == null)
            {
                nodeData = ScriptableObject.CreateInstance<T>();
                SetPosition(targetPos);
            }
            else
            {
                SetPosition(nodeData.nodePos);
            }

            userData = nodeData;
            serializedObject = new SerializedObject(nodeData);
            viewDataKey = nodeData.guid;
            mainContainer.Add(mainElement);
            RefreshPorts();
            DataBind();
        }

        /// <summary>
        /// Visual this node base on .uxml file
        /// </summary>
        private protected virtual void Visualization()
        {
            if (visualTree == null)
                throw new NullReferenceException($"Can't find the {visualTree}");
            visualTree.CloneTree(mainElement);
        }

        /// <summary>
        /// Serialized object bind
        /// </summary>
        private protected abstract void DataBind();
    }
}