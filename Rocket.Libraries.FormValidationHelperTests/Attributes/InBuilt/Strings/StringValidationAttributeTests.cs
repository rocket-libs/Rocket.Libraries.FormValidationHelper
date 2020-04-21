using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Rocket.Libraries.FormValidationHelperTests.Utility;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringValidationAttributeTests
    {
        
        [Theory]
        [InlineData("12345", true)]
        [InlineData("1234", false)]
        [InlineData("123", false)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public async Task TooLongStringsAreCaught(string value, bool expectError)
        {
            var validatableStringObject = new ValidatableStringObject
            {
                MaxLength_4_Value = value,
            };

            var fieldName = nameof(ValidatableStringObject.MaxLength_4_Value);

            using(var stringValidator = new BasicFormValidator<ValidatableStringObject>())
            {
                var validationErrors = await stringValidator.ValidateAsync(validatableStringObject);
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<ValidatableStringObject>(
                    validationErrors,
                    fieldName
                );
                
                if (expectError)
                {
                    ValidationErrorChecker.ErrorReported<ValidatableStringObject>(
                        validationErrors,
                        fieldName,
                        new StringMaxLength(default).ErrorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<ValidatableStringObject>(
                        validationErrors,
                        fieldName,
                        new StringMaxLength(default).ErrorMessage
                    );
                }
            }
        }

        [Theory]
        [InlineData("apple",false)]
        [InlineData("Apple",true)]
        [InlineData("Donkey",false)]
        [InlineData("donkey",true)]
        [InlineData("other",true)]
        public void StringIsInSetWorks(string item,bool expectError)
        {
            var hasError = new StringIsInSetAttribute("apple", "Donkey")
                .ValidationFailed(item);
            Assert.Equal(expectError, hasError);
        }

        [Theory]
        [InlineData("", true)]
        [InlineData(null, true)]
        [InlineData("blah", false)]
        public async Task NullableStringsCaught(string value, bool expectError)
        {
            var validatableStringObject = new ValidatableStringObject
            {
                NonNullableStringValue = value,
            };

            using(var stringValidator = new BasicFormValidator<ValidatableStringObject>())
            {
                var validationErrors = await stringValidator.ValidateAsync(validatableStringObject);
                if (expectError)
                {
                    ValidationErrorChecker.ErrorReported<ValidatableStringObject>(
                        validationErrors,
                        nameof(ValidatableStringObject.NonNullableStringValue),
                        new StringIsNonNullable().ErrorMessage);
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<ValidatableStringObject>(
                        validationErrors,
                        nameof(ValidatableStringObject.NonNullableStringValue),
                        new StringIsNonNullable().ErrorMessage);
                }
            }
        }
    }
}