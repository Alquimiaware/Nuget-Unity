﻿namespace Alquimiaware.NuGetUnity
{
    using System;
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

            var results = this.searchResult.Split('\n');
            foreach (var packageName in results)
            {
                if (string.IsNullOrEmpty(packageName))
                    continue;

                EditorGUILayout.BeginVertical();
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(packageName, EditorStyles.largeLabel);
                EditorGUILayout.Space();
                if (GUILayout.Button("Install", GUILayout.MinWidth(80)))
                    this.Install(packageName);

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void Install(string packageName)
        {
            Debug.Log(packageName);
        }

        private void Save()
        {
        }
    }
}