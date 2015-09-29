namespace Alquimiaware.NuGetUnity
{
    using System;
    using UnityEngine;

    public class CommandArgsBuilder
    {
        private Sources sources;

        public CommandArgsBuilder(Sources sources)
        {
            if (sources == null) throw new ArgumentNullException("sources");
            if (sources.IsEmpty) throw new ArgumentOutOfRangeException("sources", "It must define at least one valid source");

            this.sources = sources;
        }
    }
}