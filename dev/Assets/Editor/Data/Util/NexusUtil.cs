using System;
using System.IO;
using UnityEngine;

namespace Chatlyst.Editor
{
    internal static class NexusUtil
    {
        /// <summary>
        /// Open a file and write to it
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="content">string which write to file</param>
        /// <returns>Whether succeed</returns>
        internal static bool WriteToFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
    }
}