namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Nuget Sources Config")]
    public class Sources : ScriptableObject
    {
        [SerializeField]
        private List<string> local = new List<string>();
        [SerializeField]
        private List<string> remote = new List<string>();

        public bool IsEmpty
        {
            get
            {
                return (local == null || local.Count == 0)
                    && (remote == null || remote.Count == 0);
            }
        }

        public string[] GetAsArray()
        {
            return this.local.Concat(remote).ToArray();
        }

        public void AddLocal(string localPackage)
        {
            if (string.IsNullOrEmpty(localPackage))
                return;

            this.local.Add(localPackage);
        }

        public void AddRemote(string remotePackage)
        {
            if (string.IsNullOrEmpty(remotePackage))
                return;

            this.remote.Add(remotePackage);
        }
    }
}