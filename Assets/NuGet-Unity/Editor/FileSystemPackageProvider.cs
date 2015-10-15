namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FileSystemPackageProvider : IPackageProvider
    {
        public List<Package> GetAll(string packagesFolderPath)
        {
            if (!this.IsPackageSource(packagesFolderPath))
                throw new ArgumentOutOfRangeException("packagesFolderPath");

            return null;
        }

        public bool IsPackageSource(string packagesFolderPath)
        {
            return Directory.Exists(packagesFolderPath)
                && !IsEmpty(packagesFolderPath);
        }

        private bool IsEmpty(string folderPath)
        {
            var di = new DirectoryInfo(folderPath);
            return di.GetDirectories().Length == 0;
        }
    }
}