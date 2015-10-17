namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ClassifyPackages
    {
        private IPackageProvider packageProvider;

        public ClassifyPackages(IPackageProvider packageProvider)
        {
            this.packageProvider = packageProvider;
        }

        public ClassifiedPackages Execute(string packagesFolderPath)
        {
            if (!this.packageProvider.IsPackageSource(packagesFolderPath))
                throw new ArgumentOutOfRangeException(
                    "packagesFolderPath",
                    "The path does not point to a Package Source");

            List<Package> allPackages =
                packageProvider.GetAll(packagesFolderPath);

            var groups = allPackages.GroupBy(p => IsEditor(p));
            var editorGroup = groups.FirstOrDefault(g => g.Key == true);
            var runtimeGroup = groups.FirstOrDefault(g => g.Key == false);


            return new ClassifiedPackages()
            {
                Editor = editorGroup != null ?
                         editorGroup.ToList() :
                         new List<Package>(),
                Runtime = runtimeGroup != null ?
                          runtimeGroup.ToList() :
                          new List<Package>()
            };
        }

        private bool IsEditor(Package package)
        {
            return package.TargetLibs
                          .Any(lib => lib.ReferenceNames
                                         .Contains("UnityEditor"));
        }
    }
}
