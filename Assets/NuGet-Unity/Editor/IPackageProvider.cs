namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;

    public interface IPackageProvider
    {
        bool IsPackageSource(string packagesFolderPath);
        List<Package> GetAll(string packagesFolderPath);
    }
}