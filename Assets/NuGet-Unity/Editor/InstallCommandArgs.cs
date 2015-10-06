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

        protected override string GetMoreOptions()
        {
            string outOption =
                this.OutputDirectory != null ?
                string.Join(" ", new string[] 
                {
                    "-OutputDirectory",
                    this.OutputDirectory
                }) :
                string.Empty;

            return outOption;
        }
    }
}