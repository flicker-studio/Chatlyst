using System;
using NexusVisual.Runtime.ExtensionMethod;
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
        public T data;
        public string Guid => data.Guid;
        protected VisualElement visualElement;
        protected string uxmlPath = "BaseNvNode.uxml";


        protected virtual void Construction(T nodeData, Rect targetPos)
        {
            Visualization();
            data = nodeData ? nodeData : ScriptableObject.CreateInstance<T>();
            userData = data;
            SetPosition(nodeData == null ? targetPos : nodeData.Pos);
            mainContainer.Add(visualElement);
            DataBind();
        }


        /// <summary>
        /// Visual this node base on .uxml file
        /// </summary>
        private protected virtual void Visualization()
        {
            var uxml = EditorGUIUtility.Load(uxmlPath) as VisualTreeAsset;
            if (uxml == null)
                throw new NullReferenceException($"Can't find the {uxmlPath}");
            visualElement = new VisualElement();
            uxml.CloneTree(visualElement);
        }

        private protected abstract void DataBind();

        [Obsolete]
        public void NodeAdd(PlotSoGraphView soGraphView, Vector2 mousePos, BaseNvNode<T> node)
        {
            var rect = new Rect(mousePos, Vector2.one); // mousePos.ToNodePosition(soGraphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.visualElement);
            node.Visualization();
            soGraphView.AddElement(node);
        }

        [Obsolete]
        public static BaseNvNode<T> NodeRedraw(PlotSoGraphView soGraphView, BaseNvNode<T> node)
        {
            var rect = node.data.Pos;
            node.SetPosition(rect);
            node.mainContainer.Add(node.visualElement);
            node.Visualization();
            soGraphView.AddElement(node);
            return node;
        }
    }
}