﻿namespace Alquimiaware.NuGetUnity
{
    using System;

    public class CommandArgsBuilder
    {
        private Sources sources;

        public CommandArgsBuilder(Sources sources)
        {
            if (sources == null) throw new ArgumentNullException("sources");
            if (sources.IsEmpty) throw new ArgumentOutOfRangeException("sources", "It must define at least one valid source");

            this.sources = sources;
        }

        public string ListArgs()
        {
            return string.Join(
                " ",
                new string[]
                {
                    "list",
                    string.Format(
                        "-Source \"{0}\"",
                        string.Join(";", this.sources.GetAsArray()))
                });
        }
    }
}