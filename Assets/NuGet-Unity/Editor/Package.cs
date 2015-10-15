
namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;
    using System.Linq;

    public class Package
    {
        public Package(string name, string folderLocation, params string[] referenceNames)
        {
            this.Name = name;
            this.FolderLocation = folderLocation;
            this.ReferenceNames = referenceNames.ToList();
        }

        public string Name { get; private set; }
        public string FolderLocation { get; private set; }
        public List<string> ReferenceNames { get; private set; }

        public override string ToString()
        {
            return this.Name ?? "<UnnamedPackage>";
        }
    }
}