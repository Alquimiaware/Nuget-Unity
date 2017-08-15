namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEditor;
    using UnityEngine;

    public class FileSystemFolderCommands : IFolderCommands
    {
        public void Create(string path)
        {
            Directory.CreateDirectory(path);
            AssetDatabase.Refresh();
        }

        public void Delete(string path)
        {
            Directory.Delete(path, true);
            AssetDatabase.Refresh();
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public void Move(string sourcePath, string destPath)
        {
            Directory.Move(sourcePath, destPath);
            AssetDatabase.Refresh();
        }
    }
}