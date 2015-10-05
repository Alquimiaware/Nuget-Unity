namespace Alquimiaware.NuGetUnity
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class ListCommandArgs : CommandArgsBuilder
    {
        public ListCommandArgs(Sources sources)
            : base(sources)
        { }

        public string SearchTerms { get; internal set; }
        public bool ShowAllVersions { get; internal set; }
        public bool ShowPrerelase { get; internal set; }

        protected override string CommandName
        {
            get
            {
                return "list";
            }
        }

        protected override string GetDirectParams()
        {
            return this.SearchTerms ?? string.Empty;
        }

        protected override string GetMoreOptions()
        {
            return string.Join(" ",
                new string[]
                {
                    this.ShowAllVersions ? "-AllVersions" : string.Empty,
                    this.ShowPrerelase ? "-Prerelease" : string.Empty
                });
        }
    }
}