namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;

    public class TargetLib
    {
        public string Name { get; internal set; }
        public List<string> ReferenceNames { get; internal set; }
    }
}