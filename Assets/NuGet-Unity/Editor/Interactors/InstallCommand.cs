namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEngine;

    public class InstallCommand
    {
        private ClassifyPackages classifyCommand;
        private PackageMover pkgMover;
        private IFolderCommands folderCommands;
        private TargetDropper targetDropper;
        private DownloadPackage downloadPackage;

        public InstallCommand(
            DownloadPackage downloadPackage,
            ClassifyPackages classifyCommand,
            IFolderCommands folderCommands)
        {
            this.downloadPackage = downloadPackage;
            this.classifyCommand = classifyCommand;
            this.folderCommands = folderCommands;
            this.pkgMover = new PackageMover(this.folderCommands);
            this.targetDropper = new TargetDropper(this.folderCommands);
        }

        public string OutputDirectory { get; set; }
        public bool AllowPrerelease { get; set; }

        public void Execute(string packageName, string version)
        {
            var download = this.downloadPackage.Execute(packageName, version);

            if (!download.Succeeded)
                return;

            var classifiedPackages = this.classifyCommand.Execute(this.downloadPackage.TempDestDirectory);
            this.targetDropper.DropUnusedTargets(classifiedPackages);

            // TODO: Validation of results and confirmation of usage of fallback goes here

            pkgMover.MovePackagesToOutputFolder(classifiedPackages, this.OutputDirectory);
        }
    }
}