namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEngine;

    public class InstallCommand : NuGetCommand
    {
        private ClassifyPackages classifyCommand;
        private PackageMover pkgMover;
        private IFolderCommands folderCommands;
        private TargetDropper targetDropper;

        public InstallCommand(
            Sources sources,
            ClassifyPackages classifyCommand,
            IFolderCommands folderCommands)
            : base(sources)
        {
            this.classifyCommand = classifyCommand;
            this.folderCommands = folderCommands;
            this.pkgMover = new PackageMover(this.folderCommands);
            this.targetDropper = new TargetDropper(this.folderCommands);
        }

        public string OutputDirectory { get; set; }
        public bool AllowPrerelease { get; set; }

        public string Execute(string packageName, string version)
        {
            InstallCommandArgs installCmdArgs = GetCommandArgs(packageName, version);
            string callResult = CallNuGet(installCmdArgs.ToString());

            var classifiedPackages = this.classifyCommand.Execute(this.TempOutputDirectory);
            this.targetDropper.DropUnusedTargets(classifiedPackages);

            // TODO: Validation of results and confirmation of usage of fallback goes here

            pkgMover.MovePackagesToOutputFolder(classifiedPackages, this.OutputDirectory);
            folderCommands.Delete(this.TempOutputDirectory);

            return callResult;
        }

        private InstallCommandArgs GetCommandArgs(string packageName, string version)
        {
            var installCmdArgs = new InstallCommandArgs(this.Sources);
            installCmdArgs.PackageName = packageName;
            installCmdArgs.Version = version;
            installCmdArgs.OutputDirectory = "\"" + this.TempOutputDirectory + "\"";
            installCmdArgs.AllowPrerelease = this.AllowPrerelease;
            return installCmdArgs;
        }

        private string TempOutputDirectory
        {
            get
            {
                return Path.Combine(
                    Application.dataPath,
                    "../Library/Packages/Temp");
            }
        }
    }
}