namespace Alquimiaware.NuGetUnity.Tests
{
    using System;
    using NUnit.Framework;
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

        private Sources CreateEmptySources()
        {
            var sources = ScriptableObject.CreateInstance<Sources> ();
            return sources;
        }
    }
}