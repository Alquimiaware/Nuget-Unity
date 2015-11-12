namespace Alquimiaware.NuGetUnity.Tests
{
    using UnityEngine;
    using System.Collections;
    using NUnit.Framework;
    using System.IO;
    using System;

    [TestFixture]
    public class FileSystemFolderCommandsTests
    {
        [Test]
        public void CanCreate()
        {
            var sut = new FileSystemFolderCommands();
        }

        public class Contracts : IFolderCommandsContractTests
        {
            protected override string GetExistingFolderPath()
            {
                var workingDir = GetTestWorkingDir();
                Directory.CreateDirectory(workingDir);

                return workingDir;
            }

            protected override string GetNonExistingFolderPath()
            {
                return Path.Combine(
                    GetTestWorkingDir(), "FooBar123");
            }

            protected override IFolderCommands GetFolderCommands()
            {
                return new FileSystemFolderCommands();
            }

        }

        private static string GetTestWorkingDir()
        {
            return Path.Combine(
                Application.dataPath,
                "../Library/IFolderCommandsContractTests/");
        }
    }

}