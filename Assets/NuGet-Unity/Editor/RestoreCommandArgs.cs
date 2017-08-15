namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RestoreCommandArgs : CommandArgsBuilder
    {
        public RestoreCommandArgs(Sources sources)
            : base(sources)
        {
        }

        public string OutputDirectory { get; internal set; }
        public string PathToConfig { get; internal set; }

        protected override string CommandName
        {
            get
            {
                return "restore";
            }
        }

        protected override void AddMoreOptions()
        {
            AddOption("PackagesDirectory", string.Format("\"{0}\"", OutputDirectory));
        }

        protected override string GetDirectParams()
        {
            return PathToConfig;
        }
    }
}