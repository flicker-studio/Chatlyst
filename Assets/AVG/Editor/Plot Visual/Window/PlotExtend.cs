using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEngine;

namespace AVG.Editor.Plot_Visual
{
    [CustomEditor(typeof(PlotSo))]
    public class PlotExtend : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(40);

            if (GUILayout.Button("DataEdit", GUILayout.Height(20)))
            {
                PlotEditor.DataEdit(target as PlotSo);
            }
            
            
        }
    }
}