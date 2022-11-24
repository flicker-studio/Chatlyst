using System;
using NexusVisual.Runtime.ExtensionMethod;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    internal interface IVisible
    {
    }

    internal abstract class BaseNvNode<T> : Node
        where T : BaseSection
    {
        public T Section;
        public string Guid => Section.Guid;
        protected VisualElement VisualElement;

        /// <summary>
        /// create a visual node base on .uxml
        /// </summary>
        /// <param name="uxmlPath">visual asset</param>
        private protected virtual void CreatVisual(string uxmlPath)
        {
            var uxml = EditorGUIUtility.Load(uxmlPath) as VisualTreeAsset;
            VisualElement = new VisualElement();
            if (uxml == null) throw new NullReferenceException($"Can't find the {uxmlPath}");
            uxml.CloneTree(VisualElement);
        }

        protected abstract void NodeVisual();

        public void NodeAdd(PlotSoGraphView soGraphView, Vector2 mousePos, BaseNvNode<T> node)
        {
            var rect = mousePos.ToNodePosition(soGraphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            soGraphView.AddElement(node);
            Debug.Log("?");
        }

        public static BaseNvNode<T> NodeRedraw(PlotSoGraphView soGraphView, BaseNvNode<T> node)
        {
            //node.BaseSection = section;
            var rect = new Rect(node.Section.Pos); //node.Section.Pos.position.ToNodePosition(soGraphView));
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            soGraphView.AddElement(node);
            return node;
        }
    }
}