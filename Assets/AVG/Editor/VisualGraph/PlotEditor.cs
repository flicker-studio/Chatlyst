using UnityEditor;
using UnityEngine;

namespace AVG.Editor.VisualGraph
{
    [CustomEditor(typeof(PlotSo))]
    public class StoryObjectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(40);

            if (GUILayout.Button("Edit", GUILayout.Height(40)))
            {
                PlotEditorWindow.Edit();
            }
        }
    }
}