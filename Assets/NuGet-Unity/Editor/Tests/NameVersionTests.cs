namespace Alquimiaware.NuGetUnity.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class NameVersionTests
    {
        [Test]
        public void NameParsing()
        {
            // Point Sepparator
            AssertName("SocialSense.1.2", "SocialSense");
            AssertName("HtmlAgilityPack.1.4.6", "HtmlAgilityPack");
            AssertName("Newtonsoft.JsonResult.1.0.5778.10762", "Newtonsoft.JsonResult");
            AssertName("Google.ProtocolBuffersLite.Rpc.14.1218.1914.1097", "Google.ProtocolBuffersLite.Rpc");
            AssertName("Facebook.7.0.10-beta", "Facebook");
            AssertName("AWSSDK.CloudWatchLogs.3.2.3.1-beta", "AWSSDK.CloudWatchLogs");
            AssertName("Authentication.1.0.0-rc1-final", "Authentication");
            AssertName("Http.Features.1.0.0-rc1-final", "Http.Features");
            AssertName("Mvc.4.0.20710.0", "Mvc");

            // Space Sepparator
            AssertName("AppNet.NET 1.8", "AppNet.NET");
            AssertName("Acr.Settings 5.1.0", "Acr.Settings");
            AssertName("Facebook 7.0.10.3400", "Facebook");
            AssertName("Bifrost.JSON 1.0.0.32-rc1", "Bifrost.JSON");
            AssertName("CS.System.Utilities 2.0.0-bear-rat", "CS.System.Utilities");

            // Extra Spacing
            AssertName("   Fooo.1.0", "Fooo");
            AssertName("Bar    1.2.34-rc", "Bar");
        }

        [Test]
        public void VersionParsing()
        {
            // Point Sepparator
            AssertVersion("SocialSense.1.2", "1.2");
            AssertVersion("HtmlAgilityPack.1.4.6", "1.4.6");
            AssertVersion("Newtonsoft.JsonResult.1.0.5778.10762", "1.0.5778.10762");
            AssertVersion("Google.ProtocolBuffersLite.Rpc.14.1218.1914.1097", "14.1218.1914.1097");
            AssertVersion("Facebook.7.0.10-beta", "7.0.10-beta");
            AssertVersion("AWSSDK.CloudWatchLogs.3.2.3.1-beta", "3.2.3.1-beta");
            AssertVersion("Authentication.1.0.0-rc1-final", "1.0.0-rc1-final");
            AssertVersion("Http.Features.1.0.0-rc1-final", "1.0.0-rc1-final");
            AssertVersion("Mvc.4.0.20710.0", "4.0.20710.0");

            // Space Sepparator
            AssertVersion("AppNet.NET 1.8", "1.8");
            AssertVersion("Acr.Settings 5.1.0", "5.1.0");
            AssertVersion("Facebook 7.0.10.3400", "7.0.10.3400");
            AssertVersion("Bifrost.JSON 1.0.0.32-rc1", "1.0.0.32-rc1");
            AssertVersion("CS.System.Utilities 2.0.0-bear-rat", "2.0.0-bear-rat");

            // Extra Spacing
            AssertVersion("   Fooo.1.0", "1.0");
            AssertVersion("Bar    1.2.34-rc", "1.2.34-rc");
        }

        private void AssertName(string rawName, string expectedName)
        {
            NameVersion nameVersion = NameVersion.Parse(rawName);
            Assert.AreEqual(expectedName, nameVersion.Name);
        }

        private void AssertVersion(string rawName, string expectedVersion)
        {
            NameVersion nameVersion = NameVersion.Parse(rawName);
            Assert.AreEqual(expectedVersion, nameVersion.Version);
        }
    }
}