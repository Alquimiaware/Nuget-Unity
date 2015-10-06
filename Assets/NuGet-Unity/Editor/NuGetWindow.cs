namespace Alquimiaware.NuGetUnity
{
    using System.Diagnostics;
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class NuGetWindow : EditorWindow
    {
        [MenuItem("Window/NuGet")]
        private static void Init()
        {
            GetWindow<NuGetWindow>("NuGet").Show();
        }

        private static Sources GetSources()
        {
            var sourceAssetPath = FindFirst("t:Sources");
            return AssetDatabase.LoadAssetAtPath<Sources>(sourceAssetPath);
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