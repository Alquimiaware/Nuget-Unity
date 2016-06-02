namespace Alquimiaware.NuGetUnity
{
    using System.Text.RegularExpressions;

    public class NameVersion
    {
        public string Name { get; private set; }
        public string Version { get; private set; }

        public static NameVersion Parse(string versionedPackageName)
        {
            string input = versionedPackageName;

            string pattern = @"^(?<name>([a-zA-Z]+\w*\.?)+)(\s+|\.)(?<version>(\d+\.?){2,4}(-[\w-]+)?)";

            var match = Regex.Match(input, pattern);

            UnityEngine.Debug.Log("Input: " + input);

            if (match.Success)
            {
                
                GroupCollection groups = match.Groups;
                ////UnityEngine.Debug.LogFormat("{0} groups captured in <{1}>", groups.Count, match.Value);
                ////for (int i = 0; i < groups.Count; i++)
                ////{
                ////    UnityEngine.Debug.LogFormat("Group {0}: <{1}>", i, groups[i]);
                ////}
                UnityEngine.Debug.LogFormat("Name: <{0}>",groups["name"]);
                UnityEngine.Debug.LogFormat("Version: <{0}>",groups["version"]);
                return new NameVersion()
                {
                    Name = groups["name"].Value,
                    Version = groups["version"].Value
                };
            }


            throw new System.ArgumentOutOfRangeException();
        }
    }
}