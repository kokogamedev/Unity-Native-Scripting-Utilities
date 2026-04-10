using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace PsigenVision.Utilities.IO
{
    /// <summary>
    /// A utility class providing file and directory related operations.
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Ensures that the specified directory path exists, creating any missing directories in the path.
        /// </summary>
        /// <param name="path">The full path of the directory to ensure.</param>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log($"Directory created at path: {path}");
                AssetDatabase.Refresh(); // Refresh the AssetDatabase if working within Unity's Asset folder
            }
        }

        /// <summary>
        /// Combines the root path and directory name to form the full directory path. 
        /// NOTE: this path returns without the closing "/" in contrast with GetDirectoryRoot
        /// </summary>
        /// <param name="rootPath">The root path where the directory is located.</param>
        /// <param name="directoryName">The name of the directory.</param>
        /// <returns>The combined full directory path.</returns>
        public static string GetDirectoryPath(string rootPath, string directoryName)
        {
            string fullPath = Path.Combine(rootPath, directoryName);
            Debug.Log($"Full directory path: {fullPath}");
            return fullPath;
        }
        
        /// <summary>
        /// Combines the root path and directory name to form the full directory path. 
        /// NOTE: this path returns WITH the closing "/" in contrast with GetDirectoryPath
        /// </summary>
        /// <param name="rootPath">The root path where the directory is located.</param>
        /// <param name="directoryName">The name of the directory.</param>
        /// <returns>The combined full directory path.</returns>
        public static string GetDirectoryRoot(string rootPath, string directoryName)
        {
            string fullPath = Path.Combine(rootPath, directoryName);
            Debug.Log($"Full directory path: {fullPath}");
            return fullPath;
        }

        /// <summary>
        /// Sanitizes the given file name by replacing any invalid characters with an underscore.
        /// </summary>
        /// <param name="fileName">The file name to sanitize.</param>
        /// <returns>The sanitized file name with invalid characters replaced by underscores.</returns>
        public static string SanitizeFileName(string fileName)
        {
            return Regex.Replace(fileName, @"[^a-zA-Z0-9_\-]", "_");
        }
    }
}
