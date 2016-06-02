namespace Alquimiaware.NuGetUnity
{
    using System.Text.RegularExpressions;

    public class NameVersion
    {
        public string Name { get; private set; }
        public string Version { get; private set; }

        public static NameVersion Parse(string versionedPackageName)
        {
            string pattern = @"^\s*(?<name>([a-zA-Z]+\w*\.?)+)(\s+|\.)(?<version>(\d+\.?){2,4}(-[\w-]+)?)";
            var match = Regex.Match(versionedPackageName, pattern);

            if (!match.Success)
                throw new System.ArgumentOutOfRangeException(
                    "versionedPackageName",
                    string.Concat("<", versionedPackageName, "> is not a versioned package name"));


            return new NameVersion()
            {
                Name = match.Groups["name"].Value,
                Version = match.Groups["version"].Value
            };
        }
    }
}