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
        public void ToString_NotAllVersions_OptionNotPresent()
        {
            ListCommandArgs sut = DefaultListCommandArgs();
            sut.ShowAllVersions = false;
            var args = sut.ToString();
            AssertDoesntContain("-AllVersions", args);
        }

        [Test]
        public void ToString_ShowPrerelease_AddsTheOption()
        {
            var sut = DefaultListCommandArgs();
            sut.ShowPrerelase = true;
            var args = sut.ToString();
            AssertContainsOption(args, "Prerelease");
        }

        [Test]
        public void ToString_NoPrerelease_OptionNotPresent()
        {
            var sut = DefaultListCommandArgs();
            sut.ShowPrerelase = false;
            var args = sut.ToString();
            AssertDoesntContain("-Prerelease", args);
        }

        [Test]
        public void ToString_MultipleOptions_SpaceSeparated()
        {
            var sut = DefaultListCommandArgs();
            sut.ShowPrerelase = true;
            sut.ShowAllVersions = true;
            var args = sut.ToString();
            AssertContainsOption(args, "Prerelease");
            AssertContainsOption(args, "AllVersions");
        }

        private ListCommandArgs DefaultListCommandArgs()
        {
            return new ListCommandArgs(DefaultSources());
        }
    }
}