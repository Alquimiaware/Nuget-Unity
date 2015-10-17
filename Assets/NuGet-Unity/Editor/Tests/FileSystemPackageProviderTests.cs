namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using NUnit.Framework;
    using System.IO;
    using System.Collections.Generic;

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

        public class SourceValidation : FileSystemPackageProviderTests
        {
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
            public void IsPackageSource_CandidateHasNoNupkg_ReturnsFalse()
            {
                CreateEmptyFolder("PkgName");
                var sut = Default();
                Assert.IsFalse(sut.IsPackageSource(SourcePath()));
            }

            [Test]
            public void IsPackageSource_CandidateHasNoLib_ReturnsFalse()
            {
                var pkgRoot = CreatePackage("PkgName", "net35");
                pkgRoot.GetDirectories("lib")[0].Delete(true);
                var sut = Default();
                Assert.IsFalse(sut.IsPackageSource(SourcePath()));
            }

            [Test]
            public void IsPackageSource_CandidateHasNoTargets_ReturnsFalse()
            {
                CreatePackage("PkgName");
                var sut = Default();
                Assert.IsFalse(sut.IsPackageSource(SourcePath()));
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
        }

        public class ValidSource : FileSystemPackageProviderTests
        {
            [Test]
            public void GetAll_OnePackage_GetsNameAndPath()
            {
                var di = CreatePackage("Foo", "net35");
                List<Package> packages = GetPackages();

                Assert.AreEqual(1, packages.Count);
                var pkg = packages[0];
                Assert.AreEqual("Foo", pkg.Name);
                Assert.AreEqual(pkg.FolderLocation, di.FullName);
            }

            private static List<Package> GetPackages()
            {
                var sut = Default();
                return sut.GetAll(SourcePath());
            }
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

        public static DirectoryInfo CreatePackage(
            string name,
            params string[] targetFrameworks)
        {
            var pkgRoot = CreateEmptyFolder(name);
            var lib = pkgRoot.CreateSubdirectory("lib");
            foreach (var targetName in targetFrameworks)
                CreateTargetFrameworkSpecific(name, targetName, lib);
            CreateFile(name + ".nupkg", pkgRoot);
            ////CreateFile("[Content_Types].xml", pkgRoot);

            return pkgRoot;
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