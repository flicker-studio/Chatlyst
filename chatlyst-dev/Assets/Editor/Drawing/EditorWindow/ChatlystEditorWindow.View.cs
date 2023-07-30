using System;
using Chatlyst.Editor.Views;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using static UnityEngine.Resources;

namespace Chatlyst.Editor
{
    partial class ChatlystEditorWindow
    {
        //Basic element
        public static ChatlystEditorWindow EditorWindow;
        public static NexusGraphView       GraphView;

        //Toolbar element
        private ToolbarMenu   _toolbarMenu;
        private ToolbarToggle _inspectorToggle;
        private ToolbarToggle _autoSaveToggle;
        private ToolbarButton _saveButton;

        /// <summary>
        ///     Load the vision component
        /// </summary>
        /// <exception cref="Exception">Can not find EditorWindow.uxml</exception>
        private void ViewLoader()
        {
            var visualTree = Load("UXML/NodeEditorWindow") as VisualTreeAsset;
            if (!visualTree) throw new Exception("Can not find EditorWindow.uxml");
            visualTree.CloneTree(rootVisualElement);
            GraphView        = rootVisualElement.Q<NexusGraphView>("GraphView");
            _toolbarMenu     = rootVisualElement.Q<ToolbarMenu>("Menu");
            _inspectorToggle = rootVisualElement.Q<ToolbarToggle>("Inspector");
            _autoSaveToggle  = rootVisualElement.Q<ToolbarToggle>("AutoSave");
            _saveButton      = rootVisualElement.Q<ToolbarButton>("Save");
        }
    }
}
