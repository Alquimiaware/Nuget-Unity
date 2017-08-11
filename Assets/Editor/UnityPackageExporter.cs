namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEditor;

    public class UnityPackageExporter
    {
        private const string LastOutputFolderKey = "NuGet_UnityPackageExporter_LastOutputFolder";

        [MenuItem("Export/Nuget Unity")]
        public static void ExportPackage()
        {
            string path = EditorUtility.SaveFilePanel(
                "Select package destination",
                EditorPrefs.GetString(LastOutputFolderKey, string.Empty),
                "NuGetUnity",
                "unitypackage");

            if (string.IsNullOrEmpty(path))
                return;

            string directory = Path.GetDirectoryName(path);
            EditorPrefs.SetString(LastOutputFolderKey, directory);

            AssetDatabase.ExportPackage("Assets/NuGet-Unity", path, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
        }

    }
}