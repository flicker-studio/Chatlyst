using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.Callbacks;

namespace NexusVisual.Editor
{
    [CustomEditor(typeof(NexusPlotImporter))]
    internal class NexusPlotImporterEditor : ScriptedImporterEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int id, int line)
        {
            var filePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(id));
            if (!FileUtilities.PathValidCheck(filePath)) return false;
            var assetGuid = AssetDatabase.AssetPathToGUID(filePath);
            var window = EditorWindow.GetWindow<NexusPlotEditorWindow>();
            window.Initialize(assetGuid);
            return true;
        }
    }
}