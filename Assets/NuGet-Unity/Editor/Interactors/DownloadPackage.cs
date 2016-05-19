namespace Alquimiaware.NuGetUnity
{
    using System.IO;
    using UnityEngine;

    public class DownloadPackage : NuGetCommand
    {
        private IFolderCommands folderCommands;

        public DownloadPackage(
            Sources sources,
            IFolderCommands folderCommands)
            : base(sources)
        {
            this.folderCommands = folderCommands;
        }

        public string Execute(string packageName, string version)
        {
            folderCommands.Delete(this.TempDestDirectory);
            InstallCommandArgs installCmdArgs = GetCommandArgs(packageName, version);
            string callResult = CallNuGet(installCmdArgs.ToString());
            return callResult;
        }

        private InstallCommandArgs GetCommandArgs(
            string packageName,
            string version)
        {
            var installCmdArgs = new InstallCommandArgs(this.Sources);
            installCmdArgs.PackageName = packageName;
            installCmdArgs.Version = version;
            installCmdArgs.OutputDirectory = "\"" + this.TempDestDirectory + "\"";
            installCmdArgs.AllowPrerelease = true;
            return installCmdArgs;
        }

        public string TempDestDirectory
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