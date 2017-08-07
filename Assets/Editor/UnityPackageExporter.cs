namespace Alquimiaware.NuGetUnity
{
    using UnityEditor;

    public class UnityPackageExporter
    {
        [MenuItem("Export/Nuget Unity")]
        public static void ExportPackage()
        {
            AssetDatabase.ExportPackage("Assets/NuGet-Unity", "NuGetUnity.unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
        }

    }
}