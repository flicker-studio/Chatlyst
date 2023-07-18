using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chatlyst.Editor
{
    public static class FileUtilities
    {
        public static bool WriteToDisk(string path, string text)
        {
            if (!PathValidCheck(path)) return false;
            while (true)
            {
                try
                {
                    File.WriteAllText(path, text);
                }
                catch (Exception e)
                {
                    if (e.GetBaseException() is UnauthorizedAccessException &&
                        (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        if (EditorUtility.DisplayDialog("File is Read-Only", path, "Make Writeable", "Cancel Save"))
                        {
                            // make writeable and retry save
                            FileInfo fileInfo = new FileInfo(path);
                            fileInfo.IsReadOnly = false;
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    Debug.LogException(e);

                    if (EditorUtility.DisplayDialog("Exception While Saving", e.ToString(), "Retry", "Cancel"))
                        continue; // retry save
                    return false;
                }

                break; // no exception, file save success!
            }

            return true;
        }

        public static bool PathValidCheck(string path)
        {
            var fullPath = Path.GetFullPath(path);
            var fileName = Path.GetFileName(path);
            return fileName.EndsWith(".nvp") && File.Exists(fullPath);
            /*throw new Exception($"{Path.GetExtension(fileName)} is unsupported extension!");
            throw new Exception($"{fullPath} is not exist!");*/
        }

        [MenuItem("Assets/Create/Nexus Visual/Create new plot")]
        public static void AssetCreate()
        {
            var index = 0;
            var path = "Assets";
            const string ext = ".nvp";
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path) || !File.Exists(path)) continue;
                path = Path.GetDirectoryName(path);
                break;
            }

            if (path == null) throw new Exception();
            while (true)
            {
                var assetPath = path + "\\New Plot " + index + ext;
                var fullPath = Path.GetFullPath(assetPath);
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
    }
}