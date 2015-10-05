namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;
    using System;
    using UnityEngine;

    [TestFixture]
    public abstract class CommandArgsBuilderTests
    {
        private const string AnyCommandName = "Any";
        private const string AnyMoreOptions = "<MoreOptions>";
        private const string AnyDirectParams = "<DirectParameters>";

        public class BaseFeatures : CommandArgsBuilderTests
        {
            [Test]
            public void Ctor_ByDefinition_RequiresSomeSource()
            {
                var nullSources = default(Sources);
                var emptySources = CreateEmptySources();
                Assert.Throws<ArgumentNullException>(
                    () => new AnyCommand(nullSources));
                Assert.Throws<ArgumentOutOfRangeException>(
                    () => new AnyCommand(emptySources));
            }

            [Test]
            public void ToString_ByDefinition_StartsWithCommandName()
            {
                var argsBuilder = CreateAnyCommandBuilder();
                string args = argsBuilder.ToString();
                Assert.AreEqual(AnyCommandName, args.Split(' ')[0]);
            }

            [Test]
            public void ToString_ByDefinition_DirectParamsFollowCommandName()
            {
                var sut = CreateAnyCommandBuilder();
                string args = sut.ToString();
                Assert.AreEqual(AnyDirectParams, args.Split(' ')[1]);
            }

            [Test]
            public void ToString_ByDefinition_ContainsExtraOptions()
            {
                var sut = CreateAnyCommandBuilder();
                string args = sut.ToString();
                AssertContains(AnyMoreOptions, args);
            }

            [Test]
            public void ToString_HadOneSource_IncludesSource()
            {
                var localRepo = @"Z:/MyRepo/";
                var anyCommand = CreateAnyCommandBuilder(localRepo);

                string anyCommandArgs = anyCommand.ToString();
                // Expected output must contain some, including ""
                // -Source "source1path;source2Path"
                // Questions:
                // - Does the order matter? In which way?
                // - What if nuget can't resolve a path
                // - What if can't reach the remote path
                AssertContainsOption(
                    anyCommandArgs,
                    "Source",
                    string.Format("\"{0}\"", localRepo));
            }

            [Test]
            public void ToString_HadSeveralSources_IncludesAllSources()
            {
                var localRepo = @"W:/CoolRepo/";
                var remoteRepo = @"http:\\mynuget.org\repo";
                var sut = CreateAnyCommandBuilder(localRepo, remoteRepo);

                string anyCommandArgs = sut.ToString();

                AssertContainsOption(
                    anyCommandArgs,
                    "Source",
                    string.Format("\"{0};{1}\"", localRepo, remoteRepo));
            }
        }

        protected void AssertContains(
            string expectedSubstring,
            string text)
        {
            if (!text.Contains(expectedSubstring))
                Assert.Fail(
                    "Expected Substring: {0}\nNot found in: {1}",
                    expectedSubstring, text);
        }

        protected void AssertDoesntContain(
            string unexpectedSubstring,
            string text)
        {
            if (text.Contains(unexpectedSubstring))
                Assert.Fail(
                    "Unexpected Substring: {0}\n Was found in: {1}",
                    unexpectedSubstring, text);
        }

        protected void AssertContainsOption(
            string args,
            string expectedOption,
            string expectedValue = null)
        {
            var option = string.Concat("-", expectedOption);
            var chunks = args.Split(
                " ".ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries);

            var optionIdx = Array.IndexOf(chunks, option);
            if (optionIdx == -1)
                Assert.Fail(
                    "Expected Option: {0}\nWas not found in: {1}",
                    option, args);

            if (expectedValue != null)
            {
                int valueIdx = optionIdx + 1;
                bool isOutOfRange = valueIdx >= chunks.Length;
                if (isOutOfRange || chunks[valueIdx] != expectedValue)
                    Assert.Fail(
                        "Expected Value For Option <{0}> : {1}\nActual:  {2}",
                        expectedOption,
                        expectedValue,
                        isOutOfRange ? "<Has no value>" : chunks[valueIdx]);
            }
        }

        private CommandArgsBuilder CreateAnyCommandBuilder(
            string localPackage = @"C:/Packages/",
            string remotePackage = null)
        {
            var sources = CreateEmptySources();
            sources.AddLocal(localPackage);
            sources.AddRemote(remotePackage);
            return new AnyCommand(sources);
        }

        private Sources CreateEmptySources()
        {
            var sources = ScriptableObject.CreateInstance<Sources>();
            return sources;
        }

        private class AnyCommand : CommandArgsBuilder
        {
            protected override string CommandName
            {
                get
                {
                    return AnyCommandName;
                }
            }

            public AnyCommand(Sources sources)
                : base(sources)
            { }

            protected override string GetMoreOptions()
            {
                return AnyMoreOptions;
            }

            protected override string GetDirectParams()
            {
                return AnyDirectParams;
            }
        }
    }
}