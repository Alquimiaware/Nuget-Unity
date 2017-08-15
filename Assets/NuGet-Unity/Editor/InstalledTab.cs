namespace Alquimiaware.NuGetUnity
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;

    public class InstalledTab
    {
        private RestoreCommand restoreCommand;
        private PackageDependencies dependencies;

        public InstalledTab(RestoreCommand restoreCommand)
        {
            this.restoreCommand = restoreCommand;
            this.dependencies = GetDependencies();
        }

        public void OnGUI()
        {
            foreach (var dependency in dependencies.direct)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(dependency.name);
                GUILayout.Label(dependency.version);
                GUILayout.Label(dependency.targetFramework);
                GUILayout.EndHorizontal();
            }

            if(GUILayout.Button("Restore"))
            {
                var dependencies = GetDependencies();
                this.restoreCommand.Execute(dependencies);
            }
        }

        private static PackageDependencies GetDependencies()
        {
            var sourceAssetPath = AssetDatabase.GUIDToAssetPath(
                AssetDatabase.FindAssets("t:PackageDependencies")[0]);
            return AssetDatabase.LoadAssetAtPath<PackageDependencies>(sourceAssetPath);
        }
    }
}