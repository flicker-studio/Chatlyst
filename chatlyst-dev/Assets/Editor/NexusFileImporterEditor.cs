using UnityEditor;
using UnityEditor.AssetImporters;


namespace Chatlyst.Editor
{
    [CustomEditor(typeof(NexusFileImporter))]
    public class NexusFileImporterEditor : ScriptedImporterEditor
    {
        public override void OnInspectorGUI()
        {


            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.TagField("");
            EditorGUILayout.EndHorizontal();
            base.OnInspectorGUI();
        }


    }
}
