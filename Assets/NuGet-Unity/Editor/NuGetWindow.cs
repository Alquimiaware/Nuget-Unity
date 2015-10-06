namespace Alquimiaware.NuGetUnity
{
    using UnityEditor;
    using UnityEngine;

    public class NuGetWindow : EditorWindow
    {
        private int tab;
        private SearchTab searchTab;

        [MenuItem("Window/NuGet")]
        private static void Init()
        {
            GetWindow<NuGetWindow>("NuGet").Show();
        }

        private void OnEnable()
        {
            var listCommand = new ListCommand(GetSources());
            this.searchTab = new SearchTab(listCommand);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            this.tab = GUILayout.Toolbar(
                this.tab,
                new string[] { "Search", "Installed" },
                GUILayout.MaxWidth(400));

            switch (this.tab)
            {
                case 0:
                    this.searchTab.OnGUI();
                    break;
                default:
                    break;
            }

            EditorGUILayout.EndVertical();
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