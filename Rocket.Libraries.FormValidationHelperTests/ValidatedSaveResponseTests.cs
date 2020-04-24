using System.Collections.Immutable;
using Rocket.Libraries.FormValidationHelper;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests
{
    public class ValidatedSaveResponseTests
    {
        [Theory]
        [InlineData(true,true)]
        [InlineData(false,false)]
        public void HasErrorsFlagIsSetCorrectly(bool hasError,bool flagValue)
        {
            var errors = ImmutableList<ValidationError>.Empty;
            var validatedSaveResponse = new ValidatedSaveResponse<object>
            {
                ValidationErrors = hasError ? errors.Add(new ValidationError()) : errors
            };

            Assert.Equal(flagValue, validatedSaveResponse.HasErrors);
        }
    }
}