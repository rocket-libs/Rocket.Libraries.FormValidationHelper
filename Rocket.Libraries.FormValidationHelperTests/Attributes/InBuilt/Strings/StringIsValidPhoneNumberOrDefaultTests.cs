using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringIsValidPhoneNumberOrDefaultTests
    {
        [Theory]
        [InlineData ("+254721111111")]
        [InlineData ("+254 721 1 1 1 111")]
        [InlineData ("")]
        [InlineData (null)]
        public void NumberWithCountryCodePasses (string phoneNumber)
        {
            var failed = new StringIsValidPhoneNumberWithCountryCodeOrDefault (string.Empty, true).ValidationFailed (phoneNumber);
            Assert.False (failed);
        }

        [Theory]
        [InlineData ("0721111111")]
        [InlineData ("0733")]
        public void NumberWithoutCountryCodeFails (string phoneNumber)
        {
            var failed = new StringIsValidPhoneNumberWithCountryCodeOrDefault (string.Empty, true).ValidationFailed (phoneNumber);
            Assert.True (failed);
        }

        [Theory]
        [InlineData ("254721111111", true, false)]
        [InlineData ("254721111111", false, true)]
        public void RequiredPlusSignIsHandledCorrectly (string phoneNumber, bool plusSignRequired, bool isValid)
        {
            var failed = new StringIsValidPhoneNumberWithCountryCodeOrDefault (string.Empty, plusSignRequired).ValidationFailed (phoneNumber);
            Assert.NotEqual (isValid, failed);
        }

        [Fact]
        public void RequiredPlusSignDoesNotMutateInputString ()
        {
            const string constPhoneNumber = "254721111111";
            var inputPhoneNumber = constPhoneNumber;
            var failed = new StringIsValidPhoneNumberWithCountryCodeOrDefault (string.Empty, false)
                .ValidationFailed (inputPhoneNumber);
            Assert.Equal (constPhoneNumber, inputPhoneNumber);
        }
    }
}