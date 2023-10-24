using System;
using System.IO;
using Chatlyst.Editor.Serialization;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Chatlyst.Editor
{
    static class NexusMacros
    {
        public const string FilenameExtension          = "nvp";
        public const string FilenameExtensionWithPoint = ".nvp";

        [MenuItem("Assets/Create/Chatlyst/Create new plot")]
        public static void AssetCreate()
        {
            int    index = 0;
            string path  = "Assets";

            foreach (var obj in Selection.GetFiltered(typeof(object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path) || !File.Exists(path)) continue;

                path = Path.GetDirectoryName(path);
                break;
            }

            if (path == null) throw new Exception();

            while (true)
            {
                string assetPath = path + "\\New Plot " + index + FilenameExtensionWithPoint;
                string fullPath  = Path.GetFullPath(assetPath);

                if (File.Exists(fullPath))
                {
                    ++index;
                    continue;
                }

                File.Create(fullPath).Close();
                AssetDatabase.ImportAsset(assetPath);
                return;
            }
        }

        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int id, int line)
        {
            var    objectName = EditorUtility.InstanceIDToObject(id);
            string filePath   = AssetDatabase.GetAssetPath(objectName);

            try
            {
                NexusFileUtilities.PathValidCheck(filePath);
            }
            catch (Exception)
            {
                return false;
            }

            string assetGuid = AssetDatabase.AssetPathToGUID(filePath);
            ChatlystEditorWindow.EditorWindow = (ChatlystEditorWindow)EditorWindow.GetWindow(typeof(ChatlystEditorWindow));
            ChatlystEditorWindow.EditorWindow.Initialize(assetGuid);

            if (ChatlystEditorWindow.EditorWindow == null)
            {
                throw new Exception("Can't init the window!");
            }

            ChatlystEditorWindow.EditorWindow.Show(true);
            return true;
        }
    }
}
