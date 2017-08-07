namespace Alquimiaware.NuGetUnity
{
    public class ListCommand : NuGetCommand
    {
        public ListCommand(Sources sources)
            : base(sources)
        { }

        public bool ShowPrerelease { get; set; }
        public bool ShowAllVersions { get; set; }

        public NuGetCommandResult Execute(string searchTerms)
        {
            var args = new ListCommandArgs(this.Sources);
            args.SearchTerms = searchTerms;
            args.ShowAllVersions = this.ShowAllVersions;
            args.ShowPrerelase = this.ShowPrerelease;

            return CallNuGet(args.ToString());
        }
    }

}