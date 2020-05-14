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
        [InlineData (4, true)]
        [InlineData (5, false)]
        [InlineData (6, false)]
        public async Task MinimumNumberValidatedCorrectly (byte value, bool expectError)
        {
            using (var validator = new BasicFormValidator<NumberTestsDummyClass> ())
            {
                var validationErrors = await validator.ValidateAsync (new NumberTestsDummyClass
                {
                    Min5 = value
                });
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<NumberTestsDummyClass> (
                    validationErrors,
                    nameof (NumberTestsDummyClass.Min5));
                Assert.Equal (expectError, hasError);
                var errorMessage = new MinimumNumberAttribute (5).ErrorMessage;
                if (expectError)
                {
                    ValidationErrorChecker.ErrorReported<NumberTestsDummyClass> (
                        validationErrors,
                        nameof (NumberTestsDummyClass.Min5),
                        errorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<NumberTestsDummyClass> (
                        validationErrors,
                        nameof (NumberTestsDummyClass.Min5),
                        errorMessage
                    );
                }
            }
        }

        [Theory]
        [InlineData (3, true)]
        [InlineData (2, false)]
        [InlineData (1, false)]
        public async Task MaximumNumberValidatedCorrectly (byte value, bool expectError)
        {
            using (var validator = new BasicFormValidator<NumberTestsDummyClass> ())
            {
                var validationErrors = await validator.ValidateAsync (new NumberTestsDummyClass
                {
                    Max2 = value
                });
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<NumberTestsDummyClass> (
                    validationErrors,
                    nameof (NumberTestsDummyClass.Max2));
                Assert.Equal (expectError, hasError);
                var errorMessage = new MaximumNumberAttribute (2).ErrorMessage;
                if (expectError)
                {
                    ValidationErrorChecker.ErrorReported<NumberTestsDummyClass> (
                        validationErrors,
                        nameof (NumberTestsDummyClass.Max2),
                        errorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<NumberTestsDummyClass> (
                        validationErrors,
                        nameof (NumberTestsDummyClass.Max2),
                        errorMessage
                    );
                }
            }
        }

        [Theory]
        [InlineData (7.00000000000001, true)]
        [InlineData (7, false)]
        public async Task NumberEqualityTestedCorrectly (decimal value, bool expectError)
        {
            using (var validator = new BasicFormValidator<NumberTestsDummyClass> ())
            {
                var validationErrors = await validator.ValidateAsync (new NumberTestsDummyClass
                {
                    Equal7 = value
                });
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<NumberTestsDummyClass> (
                    validationErrors,
                    nameof (NumberTestsDummyClass.Equal7));
                Assert.Equal (expectError, hasError);
                var errorMessage = new NumberEqualToAttribute (7).ErrorMessage;
                if (expectError)
                {
                    ValidationErrorChecker.ErrorReported<NumberTestsDummyClass> (
                        validationErrors,
                        nameof (NumberTestsDummyClass.Equal7),
                        errorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<NumberTestsDummyClass> (
                        validationErrors,
                        nameof (NumberTestsDummyClass.Equal7),
                        errorMessage
                    );
                }
            }
        }

        [Fact]
        public async Task NullValueEquatesToZero ()
        {
            using (var validator = new BasicFormValidator<NumberTestsDummyClass> ())
            {
                var validationErrors = await validator.ValidateAsync (new NumberTestsDummyClass
                {
                    NullEqual0 = null
                });
                var hasError = ValidationErrorChecker.HasValidationErrorsForField<NumberTestsDummyClass> (
                    validationErrors,
                    nameof (NumberTestsDummyClass.NullEqual0));
                Assert.True (hasError);
                var errorMessage = new NumberEqualToAttribute (1).ErrorMessage;
                ValidationErrorChecker.ErrorReported<NumberTestsDummyClass> (
                    validationErrors,
                    nameof (NumberTestsDummyClass.NullEqual0),
                    errorMessage
                );
            }
        }
    }

    class NumberTestsDummyClass
    {
        [MinimumNumber (5)]
        public long Min5 { get; set; }

        [MaximumNumber(2)]
        public long Max2 { get; set; }

        [NumberEqualTo (7)]
        public decimal Equal7 { get; set; }

        [NumberEqualTo (1)]
        public byte? NullEqual0 { get; set; }
    }
}