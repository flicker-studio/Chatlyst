using UnityEditor;
using UnityEditor.AssetImporters;


namespace Chatlyst.Editor
{
    [CustomEditor(typeof(NexusFileImporter))]
    public class NexusFileImporterEditor : ScriptedImporterEditor
    {
        public override void OnInspectorGUI()
        {
            //TODO:Add the description of .nvp file
            ApplyRevertGUI();
        }
    }
}
