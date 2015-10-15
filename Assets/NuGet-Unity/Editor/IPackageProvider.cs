namespace Alquimiaware.NuGetUnity
{
    public interface IPackageProvider
    {
        bool IsPackageSource(string packagesFolderPath);
    }
}