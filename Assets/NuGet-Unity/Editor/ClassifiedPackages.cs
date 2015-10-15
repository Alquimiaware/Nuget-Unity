namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;

    public class ClassifiedPackages
    {
        public List<Package> Runtime { get; internal set; }
        public List<Package> Editor { get; internal set; }
    }
}