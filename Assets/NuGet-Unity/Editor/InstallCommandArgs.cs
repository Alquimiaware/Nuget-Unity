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

        protected override string CommandName
        {
            get
            {
                return "install";
            }
        }

        protected override string GetDirectParams()
        {
            return string.Empty;
        }

        protected override string GetMoreOptions()
        {
            return string.Empty;
        }
    }
}