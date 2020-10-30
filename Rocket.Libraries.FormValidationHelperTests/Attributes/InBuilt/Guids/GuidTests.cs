using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Guids;
using Rocket.Libraries.FormValidationHelperTests.Utility;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Guids
{
    public class GuidTests
    {
        [Theory]
        [InlineData("",true)]
        [InlineData(5,true)]
        [InlineData("00000000-0000-0000-0000-000000000000",true)]
        [InlineData("204283b9-ff5a-4856-ac18-97244626e781", false)]
        public async Task GuidValidatedCorrectly(object value, bool expectError)
        {
            using (var validator = new BasicFormValidator<ClassWithGuid>())
            {
                var validationErrors = await validator.ValidateAsync(new ClassWithGuid
                {
                    TestValueHolder = value
                });
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<ClassWithGuid>(
                    validationErrors,
                    nameof(ClassWithGuid.TestValueHolder));
                Assert.Equal(expectError, hasError);
                if (expectError)
                {
                    ValidationErrorChecker.ErrorReported<ClassWithGuid>(
                        validationErrors,
                        nameof(ClassWithGuid.TestValueHolder),
                        new GuidHasNonDefaultValue(string.Empty).ErrorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<ClassWithGuid>(
                        validationErrors,
                        nameof(ClassWithGuid.TestValueHolder),
                        new GuidHasNonDefaultValue(string.Empty).ErrorMessage
                    );
                }
            }
        }
    }

    class ClassWithGuid
    {
        [GuidHasNonDefaultValue("")]
        public object TestValueHolder { get; set; }
    }
}