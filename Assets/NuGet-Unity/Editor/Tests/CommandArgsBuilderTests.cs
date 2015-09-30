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
        public void ListArgs_ByDefinition_IncludesSources()
        {
            var argsBuilder = CreateArgsBuilder(@"Z:/MyRepo/");
            string args = argsBuilder.ListArgs();
            // Expected output must contain some, including ""
            // -Source "source1path;source2Path"
            // Questions:
            // - Does the order matter? In which way?
            // - What if nuget can't resolve a path
            // - What if can't reach the remote path
            AssertContainsOption(args, "Source", "\"Z:/MyRepo/\"");
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

        private CommandArgsBuilder CreateArgsBuilder(string localPackage = @"C:/Packages/")
        {
            var sources = CreateEmptySources();
            sources.local = new List<string>() { localPackage };

            return new CommandArgsBuilder(sources);
        }

        private Sources CreateEmptySources()
        {
            var sources = ScriptableObject.CreateInstance<Sources>();
            return sources;
        }
    }
}