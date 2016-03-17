namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
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
        private InstallCommand installCommand;
        private bool isSearching;
        private bool wasCancelled;

        public SearchTab(
            ListCommand listCommand,
            InstallCommand installCommand)
        {
            this.listCommand = listCommand;
            this.installCommand = installCommand;
        }

        public void OnGUI()
        {
            using (GUILayoutEx.Horizontal(EditorStyles.toolbar))
            {
                GUILayout.FlexibleSpace();

                this.showPrerelease = GUILayout.Toggle(this.showPrerelease, "Show Prerelease");
                this.showAllVersions = GUILayout.Toggle(this.showAllVersions, "Show All Versions");

                using (GUILayoutEx.ChangeCheck(Save))
                {
                    this.searchTerms = GUILayoutEx.SearchField(
                        this.searchTerms,
                        GUILayout.MinWidth(100),
                        GUILayout.MaxWidth(250),
                        GUILayout.ExpandWidth(true));
                }

                if (GUILayout.Button("Search", EditorStyles.toolbarButton)
                    || KeyEvent.JustReleased(KeyCode.Return))
                {
                    this.listCommand.ShowAllVersions = this.showAllVersions;
                    this.listCommand.ShowPrerelease = this.showPrerelease;
                    this.isSearching = true;
                    this.wasCancelled = false;
                    this.EnqueueBackgroundAction(() =>
                    {
                        this.searchResult = this.listCommand.Execute(this.searchTerms);
                        this.isSearching = false;
                    });
                }
            }

            if (this.isSearching)
            {
                this.wasCancelled = EditorUtility.DisplayCancelableProgressBar(
                    "Searching", "Searching for '" + searchTerms + "'", .995f);
                if (wasCancelled)
                {
                    this.isSearching = false;
                    EditorUtility.ClearProgressBar();
                }
            }
            else if (!wasCancelled)
            {
                RenderResultList();
                EditorUtility.ClearProgressBar();
            }
        }

        private void EnqueueBackgroundAction(Action action)
        {
            ThreadPool.QueueUserWorkItem(_ => action());
        }

        private void RenderResultList()
        {
            using (GUILayoutEx.Vertical())
            {
                this.ListScroll = EditorGUILayout.BeginScrollView(
                        this.ListScroll,
                        GUILayout.ExpandWidth(true),
                        GUILayout.MaxWidth(2000));

                var results = this.searchResult
                    .Split('\n')
                    .Where(n => !string.IsNullOrEmpty(n))
                    .Select(n => n.Trim());
                // Trim is important to remove invisible chars, that conflict with nuget

                foreach (var packageName in results)
                {
                    using (GUILayoutEx.Vertical())
                    {
                        EditorGUILayout.Space();

                        using (GUILayoutEx.Horizontal())
                        {
                            GUILayout.Label(packageName, EditorStyles.largeLabel);
                            EditorGUILayout.Space();
                            if (GUILayout.Button("Install", GUILayout.MinWidth(80)))
                                this.Install(packageName);

                            GUILayout.FlexibleSpace();
                        }
                    }
                }

                EditorGUILayout.EndScrollView();
            }
        }

        private void Install(string packageName)
        {
            var terms = packageName.Split(' ');
            var name = terms[0];
            var version = terms[1];

            this.installCommand.OutputDirectory =
                Path.Combine(Application.dataPath, "Packages/");
            this.installCommand.AllowPrerelease = this.showPrerelease;

            var output = this.installCommand.Execute(name, version);

            Debug.Log(output);
            AssetDatabase.Refresh();
        }

        private void Save()
        {

        }
    }
}