using System.IO;
using System.Linq;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
using UnityEditor;
using UnityEngine;

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

        /// <summary>
        ///     Obtain asset information based on the asset GUID
        /// </summary>
        /// <param name="assetGuid">The guid of asset</param>
        private void GetAsset(string assetGuid)
        {
            _assetGuid     = assetGuid;
            _assetPath     = AssetDatabase.GUIDToAssetPath(_assetGuid);
            _assetFullPath = Path.GetFullPath(_assetPath);
            _assetName     = Path.GetFileNameWithoutExtension(_assetPath);
            _asset         = AssetDatabase.LoadAssetAtPath<Object>(_assetPath);
        }

        /// <summary>
        ///     Read data in the asset
        /// </summary>
        /// <param name="assetGuid">The guid of asset</param>
        private void DataLoader(string assetGuid)
        {
            GetAsset(assetGuid);
            _jsonData = File.ReadAllText(_assetFullPath);
        }

        private string DataToJson(NodeIndex index)
        {
            return index.ToJson();
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            var    entityList  = GraphView.NodeEntity().ToList();
            string writeString = NexusJsonInternal.Serialize(entityList);
            FileUtilities.WriteToDisk(_assetFullPath, writeString);
        }
    }
}