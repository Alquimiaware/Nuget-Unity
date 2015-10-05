namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using System.Collections;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class InstallCommandArgsTests
        : CommandArgsBuilderTests
    {

        [Test]
        public void ToString_ByDefinition_StartsWithCommandName()
        {
            var sut = CreateDefaultInstallArgs();
            var args = sut.ToString();
            Assert.AreEqual("install", args.Split(' ')[0]);
        }

        private InstallCommandArgs CreateDefaultInstallArgs()
        {
            return new InstallCommandArgs(DefaultSources());
        }
    }
}