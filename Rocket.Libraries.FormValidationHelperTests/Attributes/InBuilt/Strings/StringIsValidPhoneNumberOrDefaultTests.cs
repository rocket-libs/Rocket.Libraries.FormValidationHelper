using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringIsValidPhoneNumberOrDefaultTests
    {
        [Theory]
        [InlineData ("+254721111111")]
        [InlineData ("+254 721 1 1 1 111")]
        [InlineData("")]
        [InlineData(null)]
        public void NumberWithCountryCodePasses (string phoneNumber)
        {
            var failed = new StringIsValidPhoneNumberWithCountryCodeOrDefault ().ValidationFailed (phoneNumber);
            Assert.False (failed);
        }

        [Theory]
        [InlineData ("0721111111")]
        [InlineData ("0733")]
        public void NumberWithoutCountryCodeFails (string phoneNumber)
        {
            var failed = new StringIsValidPhoneNumberWithCountryCodeOrDefault ().ValidationFailed (phoneNumber);
            Assert.True (failed);
        }
    }
}