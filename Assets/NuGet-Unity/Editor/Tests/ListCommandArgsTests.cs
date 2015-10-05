namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using NUnit.Framework;

    [TestFixture]
    public class ListCommandArgsTests : CommandArgsBuilderTests
    {
        [Test]
        public void ToString_ByDefinition_HasListCommandName()
        {
            var sut = DefaultListCommandArgs();
            var args = sut.ToString();
            Assert.AreEqual("list", args.Split(' ')[0]);
        }

        [Test]
        public void ToString_HasSearchTerms_IncludesSearchTerms()
        {
            string terms = "Foo Bar";
            var sut = DefaultListCommandArgs();
            sut.SearchTerms = terms;

            var args = sut.ToString();

            Assert.AreEqual("Foo", args.Split(' ')[1]);
            Assert.AreEqual("Bar", args.Split(' ')[2]);
        }

        [Test]
        public void ToString_ShowAllVersions_AddsTheOption()
        {
            ListCommandArgs sut = DefaultListCommandArgs();
            sut.ShowAllVersions = true;
            var args = sut.ToString();
            AssertContainsOption(args, "AllVersions");
        }

        [Test]
        public void ToString_ShowPrerelease_AddsTheOption()
        {
            var sut = DefaultListCommandArgs();
            sut.ShowPrerelase = true;
            var args = sut.ToString();
            AssertContainsOption(args, "Prerelease");
        }

        private ListCommandArgs DefaultListCommandArgs()
        {
            return new ListCommandArgs(DefaultSources());
        }

        private Sources DefaultSources()
        {
            var sources = ScriptableObject.CreateInstance<Sources>();
            sources.AddLocal("R:/MyLocalRepo/");
            return sources;
        }
    }
}