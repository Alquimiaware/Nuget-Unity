namespace Alquimiaware.NuGetUnity
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class PackageMover
    {
        private IFolderCommands folderCommands;

        internal PackageMover(IFolderCommands folderCommands)
        {
            this.folderCommands = folderCommands;
        }

        internal void MovePackagesToOutputFolder(ClassifiedPackages classifiedPackages, string outputDirectory)
        {
            var runtimePackages = classifiedPackages.Runtime;
            MovePackagesToFolder(runtimePackages, Path.Combine(outputDirectory, "Runtime/"));
            var editorPackages = classifiedPackages.Editor;
            MovePackagesToFolder(editorPackages, Path.Combine(outputDirectory, "Editor/"));
        }

        private void MovePackagesToFolder(List<Package> packages, string destFolder)
        {
            if (packages.Any() && !this.folderCommands.Exists(destFolder))
                this.folderCommands.Create(destFolder);

            foreach (var pkg in packages)
            {
                var sourceLocation = pkg.FolderLocation;
                var destLocation = Path.Combine(
                    destFolder,
                    pkg.Name);

                this.folderCommands.Move(sourceLocation, destLocation);
            }
        }
    }
}
