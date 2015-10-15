namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using NUnit.Framework;
    using System.IO;

    [TestFixture, Category("Integration")]
    public class FileSystemPackageProviderTests
    {
        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(SourcePath());
        }

        [TearDown]
        public void Teardown()
        {
            Directory.Delete(SourcePath(), true);
        }

        [Test]
        public void IsPackageSource_FolderNotExists_ReturnsFalse()
        {
            var sut = Default();
            var isValid = sut.IsPackageSource(
                Path.Combine(SourcePath(), "FooBarNotThere"));
            Assert.IsFalse(isValid);
        }

        [Test]
        public void IsPackageSource_EmptyFolder_ReturnsFalse()
        {
            var sut = Default();
            var isValid = sut.IsPackageSource(SourcePath());
            Assert.IsFalse(isValid);
        }

        [Test]
        public void IsPackageSource_SourceHasOnePackage_ReturnsTrue()
        {
            CreatePackage("FooBar", "net35", "net20");
            var sut = Default();
            Assert.IsTrue(sut.IsPackageSource(SourcePath()));
        }

        [Test]
        public void IsPackageSource_SourceHasSeveralPackages_ReturnsTrue()
        {
            CreatePackage("Sonic", "net35", "net20");
            CreatePackage("Ruby", "net35", "net20");
            CreatePackage("Green", "net35", "net20");
            var sut = Default();
            Assert.IsTrue(sut.IsPackageSource(SourcePath()));
        }

        [Category("Integration")]
        public class ContractTests : IPackageProviderContractTests
        {
            [SetUp]
            public void Setup()
            {
                Directory.CreateDirectory(SourcePath());
            }

            [TearDown]
            public void Teardown()
            {
                Directory.Delete(SourcePath(), true);
            }

            protected override IPackageProvider CreateProvider()
            {
                return Default();
            }

            protected override string GetInvalidSourcePath()
            {
                return SourcePath(); // Empty by default
            }

            protected override string GetValidSourcePath()
            {
                CreatePackage("KuKoo", "net35");
                return SourcePath();
            }
        }

        private static FileSystemPackageProvider Default()
        {
            return new FileSystemPackageProvider();
        }

        public static string SourcePath()
        {
            return Path.Combine(
                Application.dataPath,
                "../Library/FSTestsSource/");
        }

        public static DirectoryInfo CreateEmptyFolder(string name)
        {
            var folderPath = Path.Combine(
                SourcePath(),
                name);

            return Directory.CreateDirectory(folderPath);
        }

        public static void CreatePackage(
            string name,
            params string[] targetFrameworks)
        {
            var pkgRoot = CreateEmptyFolder(name);
            var lib = pkgRoot.CreateSubdirectory("lib");
            foreach (var targetName in targetFrameworks)
                CreateTargetFrameworkSpecific(name, targetName, lib);
            CreateFile(name + ".nupkg", pkgRoot);
            CreateFile("[Content_Types].xml", pkgRoot);
        }

        private static void CreateTargetFrameworkSpecific(
            string name,
            string targetName,
            DirectoryInfo container)
        {
            var di = container.CreateSubdirectory(targetName);
            CreateFile(name + ".dll", di);
            CreateFile(name + ".xml", di);
            CreateFile(name + ".pdb", di);
        }

        private static void CreateFile(string name, DirectoryInfo di)
        {
            File.Create(Path.Combine(di.FullName, name))
                .Dispose();
        }
    }
}