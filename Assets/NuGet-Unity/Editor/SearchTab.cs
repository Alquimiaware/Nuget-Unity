namespace Alquimiaware.NuGetUnity
{
    using UnityEditor;
    using UnityEngine;

    public class SearchTab
    {
        private string searchTerms = string.Empty;
        private bool showPrerelease = false;
        private bool showAllVersions = false;
        private string searchResult = string.Empty;
        private Vector2 ListScroll;
        private ListCommand listCommand;

        public SearchTab(ListCommand listCommand)
        {
            this.listCommand = listCommand;
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            GUILayout.FlexibleSpace();

            this.showPrerelease = GUILayout.Toggle(this.showPrerelease, "Show Prerelease");
            this.showAllVersions = GUILayout.Toggle(this.showAllVersions, "Show All Versions");

            EditorGUI.BeginChangeCheck();

            this.searchTerms = GUILayout.TextField(
                this.searchTerms,
                "ToolbarSeachTextField",
                GUILayout.MinWidth(100),
                GUILayout.MaxWidth(250),
                GUILayout.ExpandWidth(true));

            if (GUILayout.Button(GUIContent.none, string.IsNullOrEmpty(searchTerms) ? "ToolbarSeachCancelButtonEmpty" : "ToolbarSeachCancelButton"))
                searchTerms = string.Empty;

            if (EditorGUI.EndChangeCheck())
                Save();

            if (GUILayout.Button("Search", EditorStyles.toolbarButton))
            {
                this.listCommand.ShowAllVersions = this.showAllVersions;
                this.listCommand.ShowPrerelease = this.showPrerelease;
                this.searchResult =
                    this.listCommand.Execute(this.searchTerms);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();

            RenderResultList();

            EditorGUILayout.EndVertical();
        }

        private void RenderResultList()
        {
            EditorGUILayout.BeginVertical();
            this.ListScroll = EditorGUILayout.BeginScrollView(
                this.ListScroll,
                GUILayout.ExpandWidth(true),
                GUILayout.MaxWidth(2000));

            GUILayout.Label(this.searchResult);
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void Save()
        {
        }
    }
}