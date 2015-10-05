namespace Alquimiaware.NuGetUnity
{
    using System.Diagnostics;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class NuGetWindow : EditorWindow
    {
        [MenuItem("Run/Foo")]
        public static void Foo()
        {
            Sources sources = GetSources();
            var listCmdArgs = new ListCommandArgs(sources);
            var output = CallNuGet(listCmdArgs.ToString());
            UnityEngine.Debug.Log(output);
        }

        private static Sources GetSources()
        {
            var sourceAssetPath = FindFirst("t:Sources");
            return AssetDatabase.LoadAssetAtPath<Sources>(sourceAssetPath);
        }

        /// <summary>
        /// Calls nuget, blocks until completion and returns output.
        /// </summary>
        /// <param name="args">nuget.exe args to be passed</param>
        /// <returns>The output or error stream.</returns>
        private static string CallNuGet(string args)
        {
            UnityEngine.Debug.Log(args);
            var nugetFullPath = Directory.GetFiles(
                Application.dataPath,
                "nuget.exe",
                SearchOption.AllDirectories)[0];

            var startInfo = new ProcessStartInfo(nugetFullPath, args);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            var process = Process.Start(startInfo);
            var stdOut = process.StandardOutput.ReadToEnd();
            var stdError = process.StandardError.ReadToEnd();

            return !string.IsNullOrEmpty(stdOut) ?
                   stdOut :
                   stdError;
        }

        /// <summary>
        /// Searches by a pattern, returning the first asset found.
        /// </summary>
        /// <param name="filter">The seach patter, same as FindAsset.</param>
        /// <returns>Asset Relative Path to the item found.</returns>
        private static string FindFirst(string filter)
        {
            return AssetDatabase.GUIDToAssetPath(
                AssetDatabase.FindAssets(filter)[0]);
        }
    }
}