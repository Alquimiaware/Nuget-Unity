namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName ="Nuget Package Dependencies")]
    public class PackageDependencies : ScriptableObject
    {
        public List<Dependency> direct;
        [HideInInspector]
        public List<Dependency> indirect;

        [Serializable]
        public class Dependency
        {
            public string name;
            public string version;
            public string targetFramework;
        }

        public string ToXmlString()
        {
            string packageFormat = "<package id=\"{0}\" version=\"{1}\" targetFramework=\"{2}\" />";

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<packages>");
            foreach (var dependency in direct)
                sb.AppendFormat(packageFormat, dependency.name, dependency.version, dependency.targetFramework)
                  .AppendLine();
            sb.AppendLine("</packages>");
            return sb.ToString();
        }
    }
}