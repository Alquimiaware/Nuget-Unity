namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

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
            return GetPackageDirectories(di).Length == 0;
        }

        private DirectoryInfo[] GetPackageDirectories(DirectoryInfo container)
        {
            return container.GetDirectories()
                            .Where(di => IsValidPkgDirectory(di))
                            .ToArray();
        }

        private static bool IsValidPkgDirectory(DirectoryInfo di)
        {
            bool hasNupkg = di.GetFiles("*.nupkg").Length == 1;

            var libs = di.GetDirectories("lib");
            bool hasLib = libs.Length == 1;

            return hasNupkg
                && hasLib
                && libs[0].GetDirectories().Length > 0;
        }
    }
}