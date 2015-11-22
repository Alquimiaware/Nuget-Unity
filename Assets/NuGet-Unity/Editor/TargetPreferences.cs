namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;

    public class TargetPreferences
    {
        public readonly static TargetPreferences DotNetFull = new TargetPreferences(
            Net35Target,
            UnityFullTarget, UnitySubsetTarget);

        public readonly static TargetPreferences DotNetSubset = new TargetPreferences(
            Net35Target,
            UnitySubsetTarget);

        private readonly static IRuntimeCompatibility defaultCompatibility = new UnityCompatibility();

        private static IRuntimeCompatibility runtimeCompatibility;

        // Target official names
        private const string UnityFullTarget = "net35-Unity Full v3.5";
        private const string UnitySubsetTarget = "net35-Unity Subset v3.5";
        private const string Net35Target = "net35";

        private TargetPreferences(
            string fallbackTarget,
            params string[] decreasingPriorityTargets)
        {
            this.FallbackTarget = fallbackTarget;
            this.DecreasingPriorityTargets = decreasingPriorityTargets;
        }

        public IEnumerable<string> DecreasingPriorityTargets
        {
            get; private set;
        }

        public string FallbackTarget
        {
            get; private set;
        }

        internal static TargetPreferences GetEditorPrefs()
        {
            return DotNetFull;
        }

        internal static TargetPreferences GetRuntimePrefs()
        {
            return IsSubsetRuntime() ?
                   DotNetSubset :
                   DotNetFull;
        }

        public static IRuntimeCompatibility RuntimeCompatibility
        {
            get
            {
                return runtimeCompatibility ?? defaultCompatibility;
            }
            set
            {
                runtimeCompatibility = value;
            }
        }

        private static bool IsSubsetRuntime()
        {
            return RuntimeCompatibility.Level == ApiCompatibilityLevel.NET_2_0_Subset;
        }

        public interface IRuntimeCompatibility
        {
            ApiCompatibilityLevel Level { get; }
        }

        private class UnityCompatibility : IRuntimeCompatibility
        {
            public ApiCompatibilityLevel Level
            {
                get
                {
                    return PlayerSettings.apiCompatibilityLevel;
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Fallback: {0}, DecreasingPriority: {1}",
                this.FallbackTarget,
                string.Join(",", this.DecreasingPriorityTargets.ToArray()));
        }
    }
}