using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chatlyst.Editor.Serialization
{
    /// <summary>
    ///     Used to manipulate the reading and writing of strings on the hard disk
    /// </summary>
    public static class NexusFileUtilities
    {
        /// <summary>
        ///     Write a string to a hard disk file in a specific way
        /// </summary>
        /// <param name="path">The path to be written to the file ,checked by <see cref="PathValidCheck" /></param>
        /// <param name="text">The text that will be written</param>
        /// <param name="isFormat">Whether it is written in a readable form, the default is No</param>
        /// <returns>Whether the write was successful</returns>
        public static bool WriteToDisk(string path, string text, bool isFormat = false)
        {
            if (!PathValidCheck(path))
            {
                return false;
            }

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
                        if (!EditorUtility.DisplayDialog("File is Read-Only", path, "Make Writeable", "Cancel Save"))
                            return false;

                        // make writeable and retry save
                        File.SetAttributes(path, FileAttributes.Normal);
                        continue;
                    }

                    Debug.LogException(e);

                    if (EditorUtility.DisplayDialog("Exception While Saving", e.ToString(), "Retry", "Cancel"))
                    {
                        // retry save
                        continue;
                    }

                    return false;
                }

                // no exception, file save success!
                break;
            }
            return true;
        }

        /// <summary>
        ///     Check if the given path is valid
        /// </summary>
        /// <param name="path">The given path string</param>
        /// <returns>The validity of the path</returns>
        public static bool PathValidCheck(string path)
        {
            string fullPath  = Path.GetFullPath(path);
            string fileName  = Path.GetFileName(path);
            bool   checkFlag = fileName.EndsWith(".nvp") && File.Exists(fullPath);

            if (!fileName.EndsWith(".nvp"))
            {
                throw new Exception($"{Path.GetExtension(fileName)} is unsupported extension!");
            }

            if (!File.Exists(fullPath))
            {
                throw new Exception($"{fullPath} is not exist!");
            }

            return checkFlag;
        }
    }
}
