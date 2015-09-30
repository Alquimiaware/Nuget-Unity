namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using NUnit.Framework;

    [TestFixture]
    public class ListCommandArgsTests
    {
        [Test]
        public void ToString_ByDefinition_HasListCommandName()
        {
            var sources = DefaultSources();
            var sut = new ListCommandArgs(sources);
            var args = sut.ToString();
            Assert.AreEqual("list", args.Split(' ')[0]);
        }

        [Test]
        public void ToString_HasSearchTerms_IncludesSearchTerms()
        {
            string terms = "FooBar";
            var sources = DefaultSources();
            var sut = new ListCommandArgs(sources);
            sut.SearchTerms = terms;

            var args = sut.ToString();

            // TODO: Check whether several terms are valid
            // Check whether we need to quote the whole block
            Assert.AreEqual(terms, args.Split(' ')[1]);
        }

        private Sources DefaultSources()
        {
            var sources = ScriptableObject.CreateInstance<Sources>();
            sources.AddLocal("R:/MyLocalRepo/");
            return sources;
        }
    }
}