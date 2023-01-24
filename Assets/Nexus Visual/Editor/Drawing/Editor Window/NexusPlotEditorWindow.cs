using System;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace NexusVisual.Editor
{
    internal class NexusPlotEditorWindow : EditorWindow
    {
        //Data and config
        private string _assetGuid;
        private string _jsonData;
        //Basic element
        private NexusGraphView _graphView;
        //Toolbar element
        private ToolbarMenu _toolbarMenu;
        private ToolbarToggle _inspectorToggle;
        private ToolbarToggle _autoSaveToggle;
        private ToolbarButton _save;


        public void Initialize(string assetGuid)
        {
            _assetGuid = assetGuid;
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(assetGuid));
            _jsonData = File.ReadAllText(Path.GetFullPath(assetPath));

            var visualTree = Resources.Load("UXML/NodeEditorWindow") as VisualTreeAsset;
            if (!visualTree) throw new Exception("Can not find EditorWindow.uxml");
            visualTree.CloneTree(rootVisualElement);

            _graphView = rootVisualElement.Q<NexusGraphView>("GraphView");
            _toolbarMenu = rootVisualElement.Q<ToolbarMenu>("Menu");
            _inspectorToggle = rootVisualElement.Q<ToolbarToggle>("Inspector");
            _autoSaveToggle = rootVisualElement.Q<ToolbarToggle>("AutoSave");
            _save = rootVisualElement.Q<ToolbarButton>("Save");

            _graphView.GraphInitialize(this);
            _save.clicked += SaveChanges;

            TitleUpdate();
        }

        private void TitleUpdate()
        {
            if (GraphHasChangedSinceLastSerialization())
            {
                hasUnsavedChanges = true;
            }

            saveChangesMessage = "Unsaved changes!\nDo you want to save?";
            var assetPath = AssetDatabase.GUIDToAssetPath(_assetGuid);
            var assetName = Path.GetFileNameWithoutExtension(assetPath);
            titleContent.text = assetName;
        }

        private bool GraphHasChangedSinceLastSerialization()
        {
            var currentGraphJson = _graphView.ConvertToEntry();
            return !string.Equals(currentGraphJson.json, _jsonData, StringComparison.Ordinal);
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}