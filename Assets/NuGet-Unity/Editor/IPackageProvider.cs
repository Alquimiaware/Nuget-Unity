using System.Collections.Generic;

namespace Alquimiaware.NuGetUnity
{
    public interface IPackageProvider
    {
        bool IsPackageSource(string packagesFolderPath);
        List<Package> GetAll(string packagesFolderPath);
    }
}