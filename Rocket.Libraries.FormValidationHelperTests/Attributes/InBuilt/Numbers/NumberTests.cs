using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers;
using Rocket.Libraries.FormValidationHelperTests.Utility;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Numbers
{
    public class NumberTests
    {
        [Theory]
        [InlineData(4,true)]
        [InlineData(5,false)]
        [InlineData(6,false)]
        public async Task MinimumNumberValidatedCorrectly(byte value, bool expectError)
        {
            using(var validator = new BasicFormValidator<NumberTestsDummyClass>())
            {
                var validationErrors = await validator.ValidateAsync(new NumberTestsDummyClass
                {
                    Min5 = value
                });
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<NumberTestsDummyClass>(
                    validationErrors,
                    nameof(NumberTestsDummyClass.Min5));
                Assert.Equal(expectError, hasError);
                var errorMessage = new MinimumNumberAttribute(5).ErrorMessage;
                if(expectError)
                {
                    ValidationErrorChecker.ErrorReported<NumberTestsDummyClass>(
                        validationErrors,
                        nameof(NumberTestsDummyClass.Min5),
                        errorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<NumberTestsDummyClass>(
                        validationErrors,
                        nameof(NumberTestsDummyClass.Min5),
                        errorMessage
                    );
                }
            }
        }
    }

    class NumberTestsDummyClass
    {
        [MinimumNumber(5)]
        public long Min5 { get; set; }
    }
}