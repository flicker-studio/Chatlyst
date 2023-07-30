using System;
using System.IO;
using System.Linq;
using Chatlyst.Editor.Serialization;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Chatlyst.Editor
{
    partial class ChatlystEditorWindow
    {
        //Json data
        private string _jsonData;

        //Asset information
        private Object _asset;
        private string _assetGuid;
        private string _assetPath;
        private string _assetName;
        private string _assetFullPath;

        #region Read
        private void GetAsset(string assetGuid)
        {
            _assetGuid     = assetGuid;
            _assetPath     = AssetDatabase.GUIDToAssetPath(_assetGuid);
            _assetFullPath = Path.GetFullPath(_assetPath);
            _assetName     = Path.GetFileNameWithoutExtension(_assetPath);
            _asset         = AssetDatabase.LoadAssetAtPath<Object>(_assetPath);
        }

        private void DataLoader(string assetGuid)
        {
            GetAsset(assetGuid);
            _jsonData = File.ReadAllText(_assetFullPath);
        }

        private bool RebuildFromDisk()
        {
            var entityIEnumerable = NexusJsonInternal.Deserialize(_jsonData);
            if (entityIEnumerable == null) throw new Exception("Deserialize failed!");
            var entityList = entityIEnumerable.ToList();
            return GraphView.BuildFromEntries(entityList);
        }
        #endregion

        #region Write
        private bool GraphHasChangedSinceLastSerialization()
        {
            //Todo:Performance must be optimized!
            var    entityList  = GraphView.NodeEntity().ToList();
            string writeString = NexusJsonInternal.Serialize(entityList);
            return !string.Equals(writeString, _jsonData, StringComparison.Ordinal);
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            var    entityList  = GraphView.NodeEntity().ToList();
            string writeString = NexusJsonInternal.Serialize(entityList);
            FileUtilities.WriteToDisk(_assetFullPath, writeString);
        }
        #endregion

        private void WindowConfig()
        {
            // if (GraphHasChangedSinceLastSerialization())
            // {
            //     hasUnsavedChanges = true;
            // }

            saveChangesMessage = "Unsaved changes!\nDo you want to save?";
            titleContent.text  = _assetName;
        }
    }
}
