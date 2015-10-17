
namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;
    using System.Linq;

    public class Package
    {
        public Package(string name, string folderLocation, params TargetLib[] targetLibs)
        {
            this.Name = name;
            this.FolderLocation = folderLocation;
            this.TargetLibs = targetLibs.ToList();
        }

        public string Name { get; private set; }
        public string FolderLocation { get; private set; }
        public List<TargetLib> TargetLibs { get; internal set; }

        public override string ToString()
        {
            return this.Name ?? "<UnnamedPackage>";
        }
    }
}