namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;
    using System;
    using NSubstitute;

    [TestFixture]
    public class ClassifyPackagesTests
    {
        [Test]
        public void Execute_PathToInvalidSource_Fails()
        {
            var packProvider = Substitute.For<IPackageProvider>();
            packProvider.IsPackageSource(Arg.Any<string>())
                        .Returns(false);

            var sut = Default(packageProvider: packProvider);
            string anyPath = "path/to/somewhere";
            Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.Execute(anyPath));
        }

        [Test]
        public void CanCreate()
        {
            var sut = Default();
        }

        private static ClassifyPackages Default(
            IPackageProvider packageProvider = null)
        {
            packageProvider = packageProvider ??
                Substitute.For<IPackageProvider>();

            return new ClassifyPackages(packageProvider);
        }
    }

}