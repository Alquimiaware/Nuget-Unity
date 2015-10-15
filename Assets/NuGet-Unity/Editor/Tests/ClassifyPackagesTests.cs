namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;
    using System;
    using NSubstitute;
    using System.Collections.Generic;

    [TestFixture]
    public class ClassifyPackagesTests
    {
        private const string AnyPath = "path/to/somewhere";

        [Test]
        public void Execute_PathToInvalidSource_Fails()
        {
            var packProvider = Substitute.For<IPackageProvider>();
            packProvider.IsPackageSource(Arg.Any<string>())
                        .Returns(false);

            var sut = Default(packageProvider: packProvider);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.Execute(AnyPath));
        }

        [Test]
        public void Execute_OneEditorPackage_IsClassified()
        {
            Package editorPkg = EditorPkg();
            var classify = DefaultWithPackages(editorPkg);
            var classified = classify.Execute(AnyPath);

            CollectionAssert.IsEmpty(classified.Runtime);
            CollectionAssert.AreEquivalent(
                Packages(editorPkg),
                classified.Editor);
        }

        [Test]
        public void Execute_OnRuntimePackage_IsClassified()
        {
            var runtimePkg = RuntimePkg();
            var sut = DefaultWithPackages(runtimePkg);
            var result = sut.Execute(AnyPath);

            CollectionAssert.IsEmpty(result.Editor);
            CollectionAssert.AreEquivalent(
                Packages(runtimePkg),
                result.Runtime);
        }

        [Test]
        public void Execute_MixedRuntimeAndEditor_AreClassified()
        {
            var runtimeA = RuntimePkg("RuntimeA");
            var runtimeB = RuntimePkg("RuntimeB");
            var editorA = EditorPkg("EditorA");
            var editorB = EditorPkg("EditorB");
            var sut = DefaultWithPackages(runtimeA, editorA, runtimeB, editorB);
            var result = sut.Execute(AnyPath);

            CollectionAssert.AreEquivalent(
                Packages(editorA, editorB)
                , result.Editor);
            CollectionAssert.AreEquivalent(
                Packages(runtimeB, runtimeA), // Order irrelevant to spec
                result.Runtime);
        }

        private static Package[] Packages(params Package[] packages)
        {
            return packages;
        }

        private static Package EditorPkg(string name = null)
        {
            return new Package(name, AnyPath, "UnityEditor");
        }

        private static Package RuntimePkg(string name = null)
        {
            return new Package(name, AnyPath);
        }

        private static ClassifyPackages DefaultWithPackages(
            params Package[] packages)
        {
            var pkgProvider = Substitute.For<IPackageProvider>();
            pkgProvider.GetAll(AnyPath).Returns(new List<Package>(packages));
            pkgProvider.IsPackageSource(Arg.Any<string>()).Returns(true);

            return Default(pkgProvider);
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