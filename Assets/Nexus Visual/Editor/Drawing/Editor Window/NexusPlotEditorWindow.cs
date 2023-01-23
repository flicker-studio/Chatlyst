using System;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    internal class NexusPlotEditorWindow : EditorWindow
    {
        //Data and config
        private NexusPlot _nexusPlot;
        private const KeyCode MenuKey = KeyCode.Space;

        //Basic element
        private PlotSoGraphView _graphView;
        private static NexusPlotEditorWindow _window;

        //Toolbar element
        private ToolbarMenu _toolbarMenu;
        private ToolbarToggle _inspectorToggle;
        private ToolbarToggle _autoSaveToggle;
        private ToolbarButton _save;


        public void WindowInitialize()
        {
            #region Initialize editor window

            _window = this;
            var visualTree = Resources.Load("UXML/NodeEditorWindow") as VisualTreeAsset;
            if (!visualTree) throw new Exception("Can not find EditorWindow.uxml");
            visualTree.CloneTree(rootVisualElement);
            saveChangesMessage = "Unsaved changes!\nDo you want to save?";
            titleContent.text = $"{_nexusPlot}";

            #endregion

            #region Get visual element

            _graphView = rootVisualElement.Q<PlotSoGraphView>("GraphView");
            _toolbarMenu = rootVisualElement.Q<ToolbarMenu>("Menu");
            _inspectorToggle = rootVisualElement.Q<ToolbarToggle>("Inspector");
            _autoSaveToggle = rootVisualElement.Q<ToolbarToggle>("AutoSave");
            _save = rootVisualElement.Q<ToolbarButton>("Save");

            #endregion

            #region Action bind

            _graphView.RegisterCallback<KeyDownEvent>(SearchTreeBuild);
            _graphView.graphViewChanged += OnGraphViewGraphViewChanged;
            _save.clicked += SaveChanges;
            ToolBarMenuAction();

            #endregion

            #region NodeRebuild

            NodeRebuild();

            #endregion
        }

        private GraphViewChange OnGraphViewGraphViewChanged(GraphViewChange _)
        {
            hasUnsavedChanges = true;
            return default;
        }

        //Todo:Use cache to rebuild faster
        private void NodeRebuild()
        {
           
        }

        private void PlotSave()
        {
           
        }
        
        private void SearchTreeBuild(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            //create a search windows under the cursor
            var worldMousePosition = _window.position.position + Event.current.mousePosition;
            var searchWindowContext = new SearchWindowContext(worldMousePosition);
            var searchWindowProvider = CreateInstance<NodeSearchWindowProvider>();
            searchWindowProvider.Init(_graphView, _window);
            SearchWindow.Open(searchWindowContext, searchWindowProvider);
        }

        private void ToolBarMenuAction()
        {
            _toolbarMenu.menu.AppendAction("Test", _ => { Debug.Log("Test Successful"); });
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            PlotSave();
        }
        
    }
}