namespace Alquimiaware.NuGetUnity.Tests
{
    using NSubstitute;
    using NUnit.Framework;
    using UnityEditor;

    [TestFixture]
    public class TargetPreferencesTests
    {
        [Test]
        public void GetEditorPrefs_PlayerIsSubset_ExpectedFullPreferences()
        {
            TargetPreferences.RuntimeCompatibility = SubsetCompatibility();
            var prefs = TargetPreferences.GetEditorPrefs();

            Assert.AreEqual(TargetPreferences.DotNetFull, prefs);
        }

        [Test]
        public void GetEditorPrefs_PlayerIsFull_ExpectedFullPreferences()
        {
            TargetPreferences.RuntimeCompatibility = FullCompatibility();
            var prefs = TargetPreferences.GetEditorPrefs();

            Assert.AreEqual(TargetPreferences.DotNetFull, prefs);
        }

        [Test]
        public void GetRuntimePrefs_PlayerIsSubset_ExpectedSubsetPrefs()
        {
            TargetPreferences.RuntimeCompatibility = SubsetCompatibility();
            var prefs = TargetPreferences.GetRuntimePrefs();

            Assert.AreEqual(TargetPreferences.DotNetSubset, prefs);
        }

        [Test]
        public void GetRuntimePrefs_PlayerIsFull_ExpectedFullPreferences()
        {
            TargetPreferences.RuntimeCompatibility = FullCompatibility();
            var prefs = TargetPreferences.GetRuntimePrefs();

            Assert.AreEqual(TargetPreferences.DotNetFull, prefs);
        }

        [Test]
        public void RuntimeCompatibility_ByDefault_ReturnsUnityApiCompatibility()
        {
            TargetPreferences.RuntimeCompatibility = null;
            var expected = PlayerSettings.apiCompatibilityLevel;
            var actual = TargetPreferences.RuntimeCompatibility.Level;

            Assert.AreEqual(expected, actual);
        }

        private static TargetPreferences.IRuntimeCompatibility FullCompatibility()
        {
            var alwaysFullCompatibility = Substitute.For<TargetPreferences.IRuntimeCompatibility>();
            alwaysFullCompatibility.Level.Returns(ApiCompatibilityLevel.NET_2_0);
            return alwaysFullCompatibility;
        }

        private static TargetPreferences.IRuntimeCompatibility SubsetCompatibility()
        {
            var alwaysSubsetCompatibility = Substitute.For<TargetPreferences.IRuntimeCompatibility>();
            alwaysSubsetCompatibility.Level.Returns(ApiCompatibilityLevel.NET_2_0_Subset);
            return alwaysSubsetCompatibility;
        }
    }
}