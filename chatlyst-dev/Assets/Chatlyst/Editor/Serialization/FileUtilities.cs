using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chatlyst.Editor.Serialization
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
                            var fileInfo = new FileInfo(path)
                                           {
                                               IsReadOnly = false
                                           };
                            continue;
                        }
                        return false;
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
            string fullPath = Path.GetFullPath(path);
            string fileName = Path.GetFileName(path);
            return fileName.EndsWith(".nvp") && File.Exists(fullPath);
            /*throw new Exception($"{Path.GetExtension(fileName)} is unsupported extension!");
            throw new Exception($"{fullPath} is not exist!");*/
        }
    }
}
