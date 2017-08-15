namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class NuGetWindow : EditorWindow
    {
        private int tab;
        private SearchTab searchTab;
        private InstalledTab installedTab;

        [MenuItem("Window/NuGet")]
        private static void Init()
        {
            GetWindow<NuGetWindow>("NuGet").Show();
        }

        private void OnEnable()
        {
            string packageOutputDir = Path.Combine(Application.dataPath, "Packages/");
            var sources = GetSources();
            var listCommand = new ListCommand(sources);
            var fsPackageProvider = new FileSystemPackageProvider();
            var folderCommands = new FileSystemFolderCommands();
            var classifyPackages = new ClassifyPackages(fsPackageProvider);
            var downloadPackage = new DownloadPackage(sources, folderCommands);
            downloadPackage.OutputVerbosity = NuGetCommand.Verbosity.Detailed;
            var installCommand = new InstallCommand(
                downloadPackage,
                classifyPackages,
                new FileSystemFolderCommands());
            var restoreCommand = new RestoreCommand(classifyPackages, folderCommands, sources);
            installCommand.OutputDirectory = packageOutputDir;
            restoreCommand.OutputDirectory = packageOutputDir;

            this.searchTab = new SearchTab(listCommand, installCommand);
            this.installedTab = new InstalledTab(restoreCommand);
        }

        private void OnGUI()
        {
            using (GUILayoutEx.Vertical())
            {
                this.tab = GUILayout.Toolbar(
                    this.tab,
                    new string[] { "Search", "Installed" },
                    GUILayout.MaxWidth(400));

                switch (this.tab)
                {
                    case 0:
                        this.searchTab.OnGUI();
                        break;
                    case 1:
                        this.installedTab.OnGUI();
                        break;
                    default:                            
                        break;
                }
            }

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

        private void OnInspectorUpdate()
        {
            // Needed for the progress bars to work
            // If not included, progress bar will not update
            // There's at least one in SearchTab
            Repaint();
        }
    }
}