namespace Alquimiaware.NuGetUnity
{
    public class NameVersion
    {
        public string Name { get; private set; }
        public string Version { get; private set; }

        public static NameVersion Parse(string versionedPackageName)
        {
            var terms = versionedPackageName.Split(' ');
            var name = terms[0];
            var version = terms[1];

            return new NameVersion()
            {
                Name = name,
                Version = version
            };
        }
    }
}