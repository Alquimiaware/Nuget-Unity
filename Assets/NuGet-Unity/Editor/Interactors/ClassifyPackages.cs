namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ClassifyPackages
    {
        private IPackageProvider packageProvider;

        public ClassifyPackages(IPackageProvider packageProvider)
        {
            this.packageProvider = packageProvider;
        }

        public void Execute(string packagesFolderPath)
        {
            if (!this.packageProvider.IsPackageSource(packagesFolderPath))
                throw new ArgumentOutOfRangeException(
                    "packagesFolderPath",
                    "The path does not point to a Package Source");
        }
    }
}
