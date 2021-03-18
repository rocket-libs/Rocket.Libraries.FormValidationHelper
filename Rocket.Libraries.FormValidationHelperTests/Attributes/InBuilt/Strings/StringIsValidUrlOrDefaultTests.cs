using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringIsValidUrlOrDefaultTests
    {
        [Theory]
        [InlineData("https://example.com",true)]
        [InlineData("not-url.com",false)]
        [InlineData("",true)]
        [InlineData(null,true)]
        public void UriValidationWorksCorrectly(string candidate,bool shouldEvaluateToValid)
        {
            var failed = new StringIsValidUrlOrDefault(string.Empty, acceptHTTP: false)
                .ValidationFailed(candidate);

            var evaluatedAsValid = failed == false;
            Assert.Equal(shouldEvaluateToValid, evaluatedAsValid);
        }

        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void HTTPSchemeOnlyAcceptedWhenExplicitlyRequested(bool acceptHTTP, bool shouldEvaluateToValid)
        {
            var failed = new StringIsValidUrlOrDefault(string.Empty, acceptHTTP: acceptHTTP)
                .ValidationFailed("http://example.com");

            var evaluatedAsValid = failed == false;
            Assert.Equal(shouldEvaluateToValid, evaluatedAsValid);
        }
    }
}