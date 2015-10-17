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

            var folders = Directory.GetDirectories(packagesFolderPath)
                                   .Select(path => new DirectoryInfo(path));

            return folders.Select(di => new Package(
                                 di.Name,
                                 di.FullName,
                                 GetTargetLibs(di)))
                          .ToList();
        }

        private TargetLib[] GetTargetLibs(DirectoryInfo pkgContainger)
        {
            var libDir = pkgContainger.GetDirectories("lib")[0];
            var targetDirs = libDir.GetDirectories();
            return targetDirs.Select(di => new TargetLib()
            {
                Name = di.Name,
                FolderLocation = di.FullName
            }).ToArray();
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