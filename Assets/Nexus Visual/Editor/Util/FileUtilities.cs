using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace NexusVisual.Editor
{
    public static class FileUtilities
    {
        public static bool WriteToDisk(string path, string text)
        {
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

        public static string PathValidCheck(string path)
        {
            var fullPath = Path.GetFullPath(path);
            var fileName = Path.GetFileName(path);

            if (!fileName.EndsWith(".nvp"))
            {
                throw new Exception($"{Path.GetExtension(fileName)} is unsupported extension!");
            }

            if (!File.Exists(fullPath))
            {
                throw new Exception($"{fullPath} is not exist!");
            }

            return fullPath;
        }
    }
}