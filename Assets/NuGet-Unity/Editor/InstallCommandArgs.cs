using System;

namespace Alquimiaware.NuGetUnity
{
    public class InstallCommandArgs
        : CommandArgsBuilder
    {
        public InstallCommandArgs(Sources sources)
            : base(sources)
        {
        }

        public string PackageName { get; internal set; }
        public string OutputDirectory { get; internal set; }
        public string Version { get; internal set; }
        public bool AllowPrerelease { get; internal set; }
        public bool IsNonInteractive { get; internal set; }

        protected override string CommandName
        {
            get
            {
                return "install";
            }
        }

        protected override string GetDirectParams()
        {
            return this.PackageName ?? string.Empty;
        }

        protected override void AddMoreOptions()
        {
            if (this.OutputDirectory != null)
                this.AddOption("OutputDirectory", this.OutputDirectory);
            if (this.Version != null)
                this.AddOption("Version", this.Version);
            if (this.AllowPrerelease)
                this.AddOption("Prerelease");
            if (this.IsNonInteractive)
                this.AddOption("NonInteractive");
        }
    }
}