namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;

    [TestFixture, Category("Integration")]
    public abstract class IFolderCommandsContractTests
    {
        [Test]
        public void Exists_OfExistingFolder_IsTrue()
        {
            var sut = GetFolderCommands();
            var existingFolderPath = GetExistingFolderPath();

            bool exists = sut.Exists(existingFolderPath);
            Assert.IsTrue(exists);
        }

        [Test]
        public void Exists_OfNonExistingFolder_IsFalse()
        {
            var sut = GetFolderCommands();
            var existingFolderPath = GetExistingFolderPath();

            bool exists = sut.Exists(existingFolderPath);
            Assert.IsTrue(exists);
        }

        [Test]
        public void GetExistingFolderPath_ByDefinition_ReturnsExistingFolderPath()
        {
            var path = GetExistingFolderPath();
            var sut = GetFolderCommands();
            Assert.IsTrue(sut.Exists(path));
        }

        [Test]
        public void GetNonExistingFolderPath_ByDefinition_RetunrsNonExistingPath()
        {
            var path = GetNonExistingFolderPath();
            var sut = GetFolderCommands();
            Assert.IsFalse(sut.Exists(path));
        }

        protected abstract IFolderCommands GetFolderCommands();
        protected abstract string GetExistingFolderPath();
        protected abstract string GetNonExistingFolderPath();
    }
}