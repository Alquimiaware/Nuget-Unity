namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEngine;

    public class FileSystemFolderCommands : IFolderCommands
    {
        public void Create(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void Delete(string path)
        {
            Debug.Log(path);
            Directory.Delete(path, true);
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public void Move(string sourcePath, string destPath)
        {
            Directory.Move(sourcePath, destPath);
        }
    }
}