namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Nuget Sources Config")]
    public class Sources : ScriptableObject
    {
        public List<string> local;
        public List<string> remote;

        public bool IsEmpty
        {
            get
            {
                return (local == null || local.Count == 0)
                    && (remote == null || remote.Count == 0);
            }
        }
    }
}