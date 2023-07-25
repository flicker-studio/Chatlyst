using UnityEditor;
using UnityEditor.Callbacks;

namespace Chatlyst.Editor
{
    internal static class Macros
    {
        public const string FilenameExtension = "nvp";



        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int id, int line)
        {
            var objectName = EditorUtility.InstanceIDToObject(id);
            string filePath = AssetDatabase.GetAssetPath(objectName);
            if (!FileUtilities.PathValidCheck(filePath)) return false;

            string assetGuid = AssetDatabase.AssetPathToGUID(filePath);

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
