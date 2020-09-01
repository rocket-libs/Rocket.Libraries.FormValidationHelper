using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringIsValidEmailTests
    {
        [Theory]
        [InlineData (null, false)]
        [InlineData ("", false)]
        [InlineData ("a", true)]
        [InlineData ("a@", true)]
        [InlineData ("a@b", false)]
        [InlineData ("a@b.", false)]
        [InlineData ("a@b.c", false)]
        [InlineData ("@b", true)]
        [InlineData ("@b.", true)]
        [InlineData ("@b.c", true)]
        public void WorksCorrectly (string email, bool expectError)
        {
            var result = new StringIsValidEmailOrDefault ().ValidationFailed (email);
            Assert.Equal (expectError, result);
        }
    }
}