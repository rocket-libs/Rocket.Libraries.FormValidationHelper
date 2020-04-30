using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Objects;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Objects
{
    public class ObjectValidationTests
    {
        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void StringIsInSetWorks (bool useNullObject, bool expectError)
        {
            var hasError = new ObjectIsNotNull()
                .ValidationFailed (useNullObject ? null : new object());
            Assert.Equal (expectError, hasError);
        }
    }
}