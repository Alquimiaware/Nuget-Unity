namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public abstract class IPackageProviderContractTests
    {
        [Test]
        public void IsPackageSource_NullOrEmptyPath_ReturnsFalse()
        {
            var sut = CreateProvider();
            Assert.IsFalse(sut.IsPackageSource(null));
            Assert.IsFalse(sut.IsPackageSource(string.Empty));
        }

        [Test]
        public void GetAll_IsNotPackageSource_Fails()
        {
            var sut = CreateProvider();
            string invalidSourcePath = GetInvalidSourcePath();
            Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.GetAll(invalidSourcePath));
        }

        [Test]
        public void GetAll_IsValidSource_ReturnsNonEmptyList()
        {
            var sut = CreateProvider();
            var validPath = GetValidSourcePath();
            var packages = sut.GetAll(validPath);
            Assert.IsNotNull(packages);
            CollectionAssert.IsNotEmpty(packages);
            CollectionAssert.AllItemsAreNotNull(packages);
        }

        [Test]
        public void GetInvalidSourcePath_ByDefinition_IsAnInvalidPath()
        {
            var sut = CreateProvider();
            string invalidPath = GetInvalidSourcePath();
            Assert.IsFalse(sut.IsPackageSource(invalidPath));
        }

        [Test]
        public void GetValidSourcePath_ByDefinition_IsAValidPath()
        {
            var sut = CreateProvider();
            string validSource = GetValidSourcePath();
            Assert.IsTrue(sut.IsPackageSource(validSource));
        }

        protected abstract string GetInvalidSourcePath();
        protected abstract string GetValidSourcePath();
        protected abstract IPackageProvider CreateProvider();
    }

}