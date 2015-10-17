namespace Alquimiaware.NuGetUnity.Tests
{
    using Microsoft.CSharp;
    using NUnit.Framework;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;

    [TestFixture, Category("Integration")]
    public class FileSystemPackageProviderTests
    {
        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(SourcePath());
            EnsureSampleAssemblyExists();
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


            [Test]
            public void GetAll_OnePackage_GetsTargetDeliverables()
            {
                CreatePackage("Foo", "net35", "net20");
                List<Package> packages = GetPackages();
                var fooPkg = packages[0];
                AssertHasTarget(fooPkg, "net35");
                AssertHasTarget(fooPkg, "net20");
            }

            [Test]
            public void GetAll_OnePackage_GetsTargetRefereceNames()
            {
                var di = CreatePackage("Foo", "net40");
                var fooPkg = GetPackages()[0];

                var expected = GetSampleAssemblyReferenceNames().ToList();
                var actual = fooPkg.TargetLibs[0].ReferenceNames;
                CollectionAssert.AreEqual(expected, actual);
            }

            private static void AssertHasTarget(
                Package fooPkg,
                string expectedTargetName)
            {
                string expectedTargetLocation = Path.Combine(
                    SourcePath(),
                    fooPkg.Name + "/lib/" + expectedTargetName);

                Assert.IsNotNull(fooPkg.TargetLibs);
                var target = fooPkg.TargetLibs.FirstOrDefault(
                        l => l.Name == expectedTargetName);

                Assert.IsTrue(
                    target != null
                    && AreEquivalentPaths(
                        expectedTargetLocation,
                        target.FolderLocation),
                    string.Format(
                        "Expected target : {0}\n" +
                        "Expected location: {1}\n" +
                        "Actual target: {2}\n" +
                        "Actual location: {3}",
                        Path.GetFullPath(expectedTargetName),
                        Path.GetFullPath(expectedTargetLocation),
                        target != null ? target.Name : "<Target Not Found>",
                        target != null ? target.FolderLocation : "<Target Not Found>"));
            }

            private static bool AreEquivalentPaths(string pathA, string pathB)
            {
                return string.Equals(
                    Path.GetFullPath(pathA),
                    Path.GetFullPath(pathB),
                    StringComparison.OrdinalIgnoreCase);
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
                EnsureSampleAssemblyExists();
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

            return pkgRoot;
        }

        private static void CreateTargetFrameworkSpecific(
            string name,
            string targetName,
            DirectoryInfo container)
        {
            var di = container.CreateSubdirectory(targetName);
            File.Copy(
                GetSampleAssemblyPath(),
                Path.Combine(di.FullName, name + ".dll"));
            CreateFile(name + ".xml", di);
            CreateFile(name + ".pdb", di);
        }

        private static string GetSampleAssemblyPath()
        {
            return Path.Combine(
                Application.dataPath,
                "../Library/FileSystemTestSamples/SampleAssembly.dll");
        }

        private static void EnsureSampleAssemblyExists()
        {
            if (File.Exists(GetSampleAssemblyPath()))
                return;

            var csc = new CSharpCodeProvider();
            var compileParams = new CompilerParameters(
                GetSampleAssemblyReferenceNames());
            compileParams.OutputAssembly = GetSampleAssemblyPath();

            // Ensure dest folder exists
            Directory.CreateDirectory(
                Path.GetDirectoryName(GetSampleAssemblyPath()));

            csc.CompileAssemblyFromSource(
                compileParams,
                "class SampleConstant { const int K = 42; }");
        }

        private static string[] GetSampleAssemblyReferenceNames()
        {
            return new string[] { "System.dll", "System.Core" };
        }

        private static void CreateFile(string name, DirectoryInfo di)
        {
            File.Create(Path.Combine(di.FullName, name))
                .Dispose();
        }

        private static TargetLib TargetLib(
            string name,
            params string[] referenceNames)
        {
            return new TargetLib()
            {
                Name = name,
                ReferenceNames = referenceNames.ToList()
            };
        }
    }
}