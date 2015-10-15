namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using NUnit.Framework;
    using System.IO;

    public class FileSystemPackageProviderTests
    {
        public class ContractTests : IPackageProviderContractTests
        {
            private string testPath;
            private string emptySourcePath;
            private string validSourcePath;

            [SetUp]
            public void Setup()
            {
                this.testPath = Path.Combine(
                    Application.dataPath,
                    "../Library/FSContractTests/");
                this.emptySourcePath = Path.Combine(
                    this.testPath,
                    "EmptyPackageSource/");
                this.validSourcePath = Path.Combine(
                    this.testPath,
                    "ValidPackageSource/");

                Directory.CreateDirectory(testPath);
                Directory.CreateDirectory(emptySourcePath);
                Directory.CreateDirectory(validSourcePath);
            }

            [TearDown]
            public void TearDown()
            {
                Directory.Delete(this.testPath, true);
            }

            protected override IPackageProvider CreateProvider()
            {
                return new FileSystemPackageProvider();
            }

            protected override string GetInvalidSourcePath()
            {
                return emptySourcePath;
            }

            protected override string GetValidSourcePath()
            {
                return validSourcePath;
            }
        }
    }
}