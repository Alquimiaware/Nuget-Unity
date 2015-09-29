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

        private CommandArgsBuilder CreateArgsBuilder()
        {
            var sources = CreateEmptySources();
            sources.local = new List<string>() { @"C:/Packages/" };

            return new CommandArgsBuilder(sources);
        }

        private Sources CreateEmptySources()
        {
            var sources = ScriptableObject.CreateInstance<Sources>();
            return sources;
        }
    }
}