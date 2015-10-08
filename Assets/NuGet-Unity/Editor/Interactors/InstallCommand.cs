namespace Alquimiaware.NuGetUnity
{
    public class InstallCommand : NuGetCommand
    {
        public InstallCommand(Sources sources)
            : base(sources)
        { }

        public string OutputDir { get; set; }
        public bool AllowPrerelease { get; set; }

        public string Execute(string packageName, string version)
        {
            var installCmdArgs = new InstallCommandArgs(this.Sources);
            installCmdArgs.PackageName = packageName;
            installCmdArgs.Version = version;
            installCmdArgs.OutputDirectory = "\"" + OutputDir + "\"";
            installCmdArgs.AllowPrerelease = this.AllowPrerelease;

            return CallNuGet(installCmdArgs.ToString());
        }
    }
}