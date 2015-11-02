namespace Alquimiaware.NuGetUnity
{
    public class InstallCommand : NuGetCommand
    {
        public InstallCommand(Sources sources)
            : base(sources)
        { }

        public string OutputDirectory { get; set; }
        public bool AllowPrerelease { get; set; }

        public string Execute(string packageName, string version)
        {
            InstallCommandArgs installCmdArgs = GetCommandArgs(packageName, version);
            return CallNuGet(installCmdArgs.ToString());
        }

        private InstallCommandArgs GetCommandArgs(string packageName, string version)
        {
            var installCmdArgs = new InstallCommandArgs(this.Sources);
            installCmdArgs.PackageName = packageName;
            installCmdArgs.Version = version;
            installCmdArgs.OutputDirectory = "\"" + this.OutputDirectory + "\"";
            installCmdArgs.AllowPrerelease = this.AllowPrerelease;
            return installCmdArgs;
        }
    }
}