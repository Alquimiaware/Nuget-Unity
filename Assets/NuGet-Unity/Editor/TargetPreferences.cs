namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;

    public class TargetPreferences
    {
        public readonly static TargetPreferences DotNetFull = new TargetPreferences(
            Net35Target,
            UnityFullTarget, UnitySubsetTarget);

        public readonly static TargetPreferences DotNetSubset = new TargetPreferences(
            Net35Target,
            UnitySubsetTarget);

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
    }
}