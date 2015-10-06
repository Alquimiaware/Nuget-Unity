namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;

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

        [Test]
        public void ToString_GivenPackageName_ProvidesItAsDirectArg()
        {
            var sut = CreateDefaultInstallArgs();
            sut.PackageName = "Foo.Bar";
            var args = sut.ToString();

            Assert.AreEqual("Foo.Bar", args.Split(' ')[1]);
        }

        [Test]
        public void ToString_GivenOutputDirectory_AddsOptionAndValue()
        {
            const string optionName = "OutputDirectory";
            const string optionValue = "./Path/To/Some/Dir/";

            var sut = CreateDefaultInstallArgs();
            sut.OutputDirectory = optionValue;
            var args = sut.ToString();
            AssertContainsOption(args, optionName, optionValue);
        }

        [Test]
        public void ToSting_GivenVersion_AddsOptionAndValue()
        {
            const string optionName = "Version";
            const string optionValue = "1.2.3";

            var sut = CreateDefaultInstallArgs();
            sut.Version = optionValue;
            var args = sut.ToString();
            AssertContainsOption(args, optionName, optionValue);
        }

        [Test]
        public void ToSting_NoGivenVersion_DoesntAddOption()
        {
            const string optionName = "Version";
            var sut = CreateDefaultInstallArgs();
            var args = sut.ToString();

            AssertDoesntContain(optionName, args);
        }

        [Test]
        public void ToString_GivenPrerelease_AddsTheOption()
        {
            const string optionName = "Prerelease";
            var sut = CreateDefaultInstallArgs();
            sut.AllowPrerelease = true;
            var args = sut.ToString();
            AssertContainsOption(args, optionName);
        }

        [Test]
        public void ToString_NotGivenPrerelease_DoesntAddOption()
        {
            const string optionName = "Prerelease";
            var sut = CreateDefaultInstallArgs();
            sut.AllowPrerelease = false;
            var args = sut.ToString();
            AssertDoesntContain(optionName, args);
        }

        [Test]
        public void ToString_SetAsNonInteractive_AddsTheOption()
        {
            const string optionName = "NonInteractive";
            var sut = CreateDefaultInstallArgs();
            sut.IsNonInteractive = true;
            var args = sut.ToString();
            AssertContainsOption(args, optionName);
        }

        [Test]
        public void ToString_SetAsInteractive_DoesntAddNonInteractiveOption()
        {
            const string optionName = "NonInteractive";
            var sut = CreateDefaultInstallArgs();
            sut.IsNonInteractive = false;
            var args = sut.ToString();
            AssertDoesntContain(optionName, args);
        }

        private InstallCommandArgs CreateDefaultInstallArgs()
        {
            return new InstallCommandArgs(DefaultSources());
        }
    }
}