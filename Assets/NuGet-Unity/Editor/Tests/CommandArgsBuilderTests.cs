namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [TestFixture]
    public class CommandArgsBuilderTests
    {
        [Test]
        public void Ctor_ByDefinition_RequiresSomeSource()
        {
            var nullSources = default(Sources);
            var emptySources = CreateEmptySources();
            Assert.Throws<ArgumentNullException>(
                () => new CommandArgsBuilder(nullSources));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new CommandArgsBuilder(emptySources));
        }

        [Test]
        public void ListArgs_ByDefinition_StartsWithListCommand()
        {
            var argsBuilder = CreateArgsBuilder();
            string args = argsBuilder.ListArgs();
            Assert.IsTrue(args.Split(' ')[0] == "list");
        }

        [Test]
        public void ListArgs_HadOneSource_IncludesSource()
        {
            var localRepo = @"Z:/MyRepo/";
            var argsBuilder = CreateArgsBuilder(localRepo);

            string listArgs = argsBuilder.ListArgs();
            // Expected output must contain some, including ""
            // -Source "source1path;source2Path"
            // Questions:
            // - Does the order matter? In which way?
            // - What if nuget can't resolve a path
            // - What if can't reach the remote path
            AssertContainsOption(
                listArgs,
                "Source",
                string.Format("\"{0}\"", localRepo));
        }

        [Test]
        public void ListArgs_HadSeveralSources_IncludesAllSources()
        {
            var localRepo = @"W:/CoolRepo/";
            var remoteRepo = @"http:\\mynuget.org\repo";
            var sut = CreateArgsBuilder(localRepo, remoteRepo);

            string listArgs = sut.ListArgs();

            AssertContainsOption(
                listArgs,
                "Source",
                string.Format("\"{0};{1}\"", localRepo, remoteRepo));
        }

        private void AssertContainsOption(
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

        private CommandArgsBuilder CreateArgsBuilder(
            string localPackage = @"C:/Packages/",
            string remotePackage = null)
        {
            var sources = CreateEmptySources();
            sources.AddLocal(localPackage);
            sources.AddRemote(remotePackage);
            return new CommandArgsBuilder(sources);
        }

        private Sources CreateEmptySources()
        {
            var sources = ScriptableObject.CreateInstance<Sources>();
            return sources;
        }
    }
}