using AVG.Runtime;
using UnityEditor;
using UnityEngine;

namespace AVG.Editor
{
    [CustomEditor(typeof(PlotSo))]
    internal class PlotSoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(40);

            if (GUILayout.Button("DataEdit", GUILayout.Height(20)))
            {
                PlotSoEditorWindow.DataEdit(target as PlotSo);
            }
            
            
        }
    }
}