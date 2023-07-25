using System;
using System.IO;
using System.Linq;
using Chatlyst.Editor.Serialization;
using Chatlyst.Editor.Views;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Chatlyst.Editor
{
    internal class NexusPlotEditorWindow : EditorWindow
    {
        //Data and config
        private string _assetGuid;
        private string _jsonData;
        //Basic element
        public static NexusPlotEditorWindow EditorWindow;
        public static NexusGraphView GraphView;
        //Toolbar element
        /*
        private ToolbarMenu _toolbarMenu;
        private ToolbarToggle _inspectorToggle;
        private ToolbarToggle _autoSaveToggle;
        private ToolbarButton _save;
        */
        public void Initialize(string assetGuid)
        {
            _assetGuid = assetGuid;
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(assetGuid));
            _jsonData = File.ReadAllText(Path.GetFullPath(assetPath));
            /*
            var visualTree = Resources.Load("UXML/NodeEditorWindow") as VisualTreeAsset;
            if (!visualTree) throw new Exception("Can not find EditorWindow.uxml");
            visualTree.CloneTree(rootVisualElement);
            
            GraphView = rootVisualElement.Q<NexusGraphView>("GraphView");
            _toolbarMenu = rootVisualElement.Q<ToolbarMenu>("Menu");
            _inspectorToggle = rootVisualElement.Q<ToolbarToggle>("Inspector");
            _autoSaveToggle = rootVisualElement.Q<ToolbarToggle>("AutoSave");
            _save = rootVisualElement.Q<ToolbarButton>("Save");

            GraphView.GraphInitialize(this);
            _save.clicked += SaveChanges;
            
            TitleUpdate();
            RebuildFromDisk();
            */
        }

        private void TitleUpdate()
        {
            // if (GraphHasChangedSinceLastSerialization())
            // {
            //     hasUnsavedChanges = true;
            // }

            saveChangesMessage = "Unsaved changes!\nDo you want to save?";
            var assetPath = AssetDatabase.GUIDToAssetPath(_assetGuid);
            var assetName = Path.GetFileNameWithoutExtension(assetPath);
            titleContent.text = assetName;
        }

        private bool GraphHasChangedSinceLastSerialization()
        {
            //Todo:Performance must be optimized!
            var entityList = GraphView.NodeEntity().ToList();
            var writeString = NexusJsonInternal.Serialize(entityList);
            return !string.Equals(writeString, _jsonData, StringComparison.Ordinal);
        }

        private bool RebuildFromDisk()
        {
            var entityIEnumerable = NexusJsonInternal.Deserialize(_jsonData);
            if (entityIEnumerable == null) throw new Exception("Deserialize failed!");
            var entityList = entityIEnumerable.ToList();
            return GraphView.BuildFromEntries(entityList);
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            var entityList = GraphView.NodeEntity().ToList();
            var writeString = NexusJsonInternal.Serialize(entityList);
            var assetPath = AssetDatabase.GUIDToAssetPath(_assetGuid);
            var fullPath = Path.GetFullPath(assetPath);
            FileUtilities.WriteToDisk(fullPath, writeString);
        }

        public void Update()
        {
            /*  GraphView.graphViewChanged += _ =>
              {
                  hasUnsavedChanges = true;
                  return default;
              };
  
              if (GraphView == null)
              {
                  if (_assetGuid != null)
                  {
                      Initialize(_assetGuid);
                  }
                  else
                  {
                      Close();
                  }
              }
              else
              {
                  GraphView.InspectorNode();
              }*/
        }
    }
}
