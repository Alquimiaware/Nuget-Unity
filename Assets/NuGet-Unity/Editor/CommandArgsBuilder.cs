namespace Alquimiaware.NuGetUnity
{
    using System;

    public abstract class CommandArgsBuilder
    {
        private Sources sources;

        protected abstract string CommandName { get; }

        public CommandArgsBuilder(Sources sources)
        {
            if (sources == null) throw new ArgumentNullException("sources");
            if (sources.IsEmpty) throw new ArgumentOutOfRangeException("sources", "It must define at least one valid source");

            this.sources = sources;
        }

        public override string ToString()
        {
            return string.Join(
                " ",
                new string[]
                {
                    this.CommandName,
                    this.GetDirectParams(),
                    this.GetSourceOption(),
                    this.GetMoreOptions(),
                });
        }

        private string GetSourceOption()
        {
            return string.Format(
                "-Source \"{0}\"",
                string.Join(";", this.sources.GetAsArray()));
        }

        protected abstract string GetMoreOptions();
        protected abstract string GetDirectParams();
    }
}