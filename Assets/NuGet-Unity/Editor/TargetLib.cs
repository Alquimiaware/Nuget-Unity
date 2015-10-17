namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;

    public class TargetLib
    {
        public string FolderLocation { get; internal set; }
        public string Name { get; internal set; }
        public List<string> ReferenceNames { get; internal set; }
    }
}