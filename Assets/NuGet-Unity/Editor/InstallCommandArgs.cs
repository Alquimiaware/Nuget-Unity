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

        public string OutputDirectory { get; internal set; }
        public string PackageName { get; internal set; }

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
        }
    }
}