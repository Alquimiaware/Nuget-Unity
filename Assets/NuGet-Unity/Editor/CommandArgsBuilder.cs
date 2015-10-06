namespace Alquimiaware.NuGetUnity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class CommandArgsBuilder
    {
        private Sources sources;
        private List<Option> extraOptions = null;

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

        private string GetMoreOptions()
        {
            this.extraOptions = new List<Option>();
            this.AddMoreOptions();

            return string.Join(" ",
                this.extraOptions
                    .Select(o => o.ToString())
                    .ToArray());
        }

        protected void AddOption(string name, string value = null)
        {
            this.extraOptions.Add(new Option(name, value));
        }

        protected abstract void AddMoreOptions();
        protected abstract string GetDirectParams();

        private class Option
        {
            public Option(string name, string value)
            {
                this.name = name;
                this.value = value;
            }

            public readonly string name;
            public readonly string value;

            public override string ToString()
            {
                return string.Format(
                    "-{0} {1}",
                    name,
                    value ?? string.Empty);
            }
        }
    }
}