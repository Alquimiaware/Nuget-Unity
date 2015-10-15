namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;

    public class FileSystemPackageProvider : IPackageProvider
    {
        public List<Package> GetAll(string packagesFolderPath)
        {
            return null;
        }

        public bool IsPackageSource(string packagesFolderPath)
        {
            return true;
        }
    }
}