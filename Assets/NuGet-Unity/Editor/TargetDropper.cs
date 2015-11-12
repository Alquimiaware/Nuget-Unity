namespace Alquimiaware.NuGetUnity
{
    using UnityEngine;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    class TargetDropper
    {
        private IFolderCommands folderCommands;

        public TargetDropper(IFolderCommands folderCommands)
        {
            this.folderCommands = folderCommands;
        }

        internal void DropUnusedTargets(ClassifiedPackages classifiedPackages)
        {
            DropUnusedTargets(TargetPreferences.GetEditorPrefs(), classifiedPackages.Editor);
            DropUnusedTargets(TargetPreferences.GetRuntimePrefs(), classifiedPackages.Runtime);
        }

        private void DropUnusedTargets(TargetPreferences preferences, List<Package> packages)
        {
            foreach (var pkg in packages)
                DropUnusedTargets(preferences, pkg);
        }

        private void DropUnusedTargets(TargetPreferences preferences, Package package)
        {
            var chosenTarget = ChoosePackageTarget(package, preferences);
            // TODO: Abort install if some target could not be found
            // All the copies should be postponed until all packages can be resolved
            if (chosenTarget != null)
                DeleteNonChosenTargets(package, chosenTarget);
        }

        private void DeleteNonChosenTargets(Package package, TargetLib chosenTarget)
        {
            foreach (var target in package.TargetLibs)
            {
                if (target.Name != chosenTarget.Name)
                {
                    // TODO: Properly abstract this
                    ////Debug.LogFormat("Deleting Target '{0}' of package '{1}' @ '{2}'",
                    ////    target.Name, package.Name, target.FolderLocation);
                    this.folderCommands.Delete(target.FolderLocation);
                }
            }
        }

        private TargetLib ChoosePackageTarget(Package package, TargetPreferences prefs)
        {
            TargetLib chosenTarget = null;
            foreach (var preferedTarget in prefs.DecreasingPriorityTargets)
            {
                chosenTarget = package.TargetLibs
                    .FirstOrDefault(t => t.Name == preferedTarget);

                if (chosenTarget != null)
                    break;
            }

            // try fallback
            if (chosenTarget == null)
            {
                chosenTarget = package.TargetLibs
                    .FirstOrDefault(t => t.Name == prefs.FallbackTarget);

                if (chosenTarget != null)
                    Debug.LogWarningFormat(
                        "Package: '{0}' Installed fallback target: '{1}'. "
                       + "It's not guaranteed to work",
                        package.Name,
                        prefs.FallbackTarget);
            }

            if (chosenTarget == null)
            {
                // no other things to try
                // installation failed
                Debug.LogErrorFormat("Package: '{0}' Not Installed. No matching target.",
                    package.Name);
            }

            return chosenTarget;
        }
    }
}