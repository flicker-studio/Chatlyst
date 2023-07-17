using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.Callbacks;

namespace Chatlyst.Editor
{
    [CustomEditor(typeof(NexusPlotImporter))]
    internal class NexusPlotImporterEditor : ScriptedImporterEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.TagField("textHere");
        }

        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int id, int line)
        {
            var filePath = AssetDatabase.GetAssetPath(EditorUtility.InstanceIDToObject(id));
            if (!FileUtilities.PathValidCheck(filePath)) return false;
            var assetGuid = AssetDatabase.AssetPathToGUID(filePath);
            if (NexusPlotEditorWindow.EditorWindow == null)
            {
                NexusPlotEditorWindow.EditorWindow = EditorWindow.GetWindow<NexusPlotEditorWindow>();
                NexusPlotEditorWindow.EditorWindow.Initialize(assetGuid);
            }

            NexusPlotEditorWindow.EditorWindow.Show();
            return true;
        }
    }
}