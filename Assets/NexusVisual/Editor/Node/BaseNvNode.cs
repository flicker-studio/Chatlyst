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
    internal abstract class BaseNvNode<T> : Node
        where T : BaseSection
    {
        protected readonly VisualElement mainElement = new VisualElement();
        protected VisualTreeAsset visualTree;
        protected SerializedObject serializedObject;

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
                SetPosition(nodeData.Pos);
            }

            userData = nodeData;
            serializedObject = new SerializedObject(nodeData);
            viewDataKey = nodeData.Guid;
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

        private protected abstract void DataBind();
    }
}