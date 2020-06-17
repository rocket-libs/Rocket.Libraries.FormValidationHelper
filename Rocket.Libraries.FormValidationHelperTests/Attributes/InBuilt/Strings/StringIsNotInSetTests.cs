using System;
using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Rocket.Libraries.FormValidationHelperTests.Utility;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringIsNotInSetTests
    {
        const string defaultGuid = "00000000-0000-0000-0000-000000000000";
        [Theory]
        [InlineData(false,null)]
        [InlineData(false,"")]
        [InlineData(true,"00000000-0000-0000-0000-000000000000")]
        [InlineData(false,"Other Text")]
        public async Task StringIsNotInSetHappyPathWorks(bool expectError, string inputData)
        {
            var validationErrors = await new BasicFormValidator<TheClass>()
                .ValidateAsync(new TheClass
                {
                    MultisetExclusion = inputData
                });
            var hasError = ValidationErrorChecker.HasValidationErrorsForField<TheClass>(
                validationErrors,
                nameof(TheClass.MultisetExclusion));

            Assert.Equal(expectError, hasError);
        }

        [Fact]
        public async Task NullStringDoesCrashValidator()
        {
            var exceptionOccured = false;
            try
            {
                var singleString = new SingleString
                {
                    TheString = null,
                };
                using (var basicFormValidator = new BasicFormValidator<SingleString>())
                {
                    var validationErrors = await basicFormValidator.ValidateAsync(singleString);
                }
                
            }
            catch
            {
                exceptionOccured = true;
            }
            finally
            {
                Assert.False(exceptionOccured);
            }
        }


        [Fact]
        public async Task NullExclusionSetDoesCrashValidator()
        {
            var exceptionOccured = false;
            try
            {
                var emptyExclusionSet = new EmptyExclusionSet
                {
                    TheString = "blah",
                };
                using (var basicFormValidator = new BasicFormValidator<EmptyExclusionSet>())
                {
                    var validationErrors = await basicFormValidator.ValidateAsync(emptyExclusionSet);
                }
                
            }
            catch
            {
                exceptionOccured = true;
            }
            finally
            {
                Assert.False(exceptionOccured);
            }
        }

        [Fact]
        public async Task EmptyStringDoesCrashValidator()
        {
            var exceptionOccured = false;
            try
            {
                var singleString = new SingleString
                {
                    TheString = string.Empty,
                };
                using (var basicFormValidator = new BasicFormValidator<SingleString>())
                {
                    var validationErrors = await basicFormValidator.ValidateAsync(singleString);
                }
                
            }
            catch
            {
                exceptionOccured = true;
            }
            finally
            {
                Assert.False(exceptionOccured);
            }
        }


        [Fact]
        public async Task VerifyThatErrorGrammarIsCorrect()
        {
            const string multiSetErrorMessage = "Value '00000000-0000-0000-0000-000000000000' is not allowed for this field. Other disallowed value(s): 'alpha', 'beta' and 'charlie'.";
            const string dualSetErrorMessage = "Value '00000000-0000-0000-0000-000000000000' is not allowed for this field. Other disallowed value(s): 'alpha'.";
            const string singletonErrorMessage = "Value '00000000-0000-0000-0000-000000000000' is not allowed for this field.";
            var theClass = new TheClass
            {
                DualsetExclusion = defaultGuid,
                MultisetExclusion = defaultGuid,
                SingletonExclusion = defaultGuid
            };

            using(var basicFormValidator = new BasicFormValidator<TheClass>())
            {
                var validationErrors = await basicFormValidator.ValidateAsync(theClass);
                ValidationErrorChecker.ErrorReported<TheClass>(
                    validationErrors,
                    nameof(TheClass.MultisetExclusion),
                    multiSetErrorMessage);

                ValidationErrorChecker.ErrorReported<TheClass>(
                    validationErrors,
                    nameof(TheClass.DualsetExclusion),
                    dualSetErrorMessage);

                ValidationErrorChecker.ErrorReported<TheClass>(
                    validationErrors,
                    nameof(TheClass.SingletonExclusion),
                    singletonErrorMessage);

            }
        }

        class SingleString
        {
            [StringIsNotInSet(StringComparison.InvariantCulture,defaultGuid)]
            public string TheString { get; set; }
        }


        class EmptyExclusionSet
        {
            [StringIsNotInSet(StringComparison.InvariantCulture)]
            public string TheString { get; set; }
        }

        class TheClass
        {
            
            [StringIsNotInSet(StringComparison.InvariantCulture,defaultGuid,"alpha","beta","charlie")]
            public string MultisetExclusion { get; set; }

            
            [StringIsNotInSet(StringComparison.InvariantCulture,defaultGuid)]
            public string SingletonExclusion { get; set; }

            [StringIsNotInSet(StringComparison.InvariantCulture,defaultGuid,"alpha")]
            public string DualsetExclusion { get; set; }
        }
    }
}