using System;
using System.Collections.Immutable;
using Rocket.Libraries.FormValidationHelper;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes
{
    public class ValidatorFunctionsGeneratorTests
    {
        [Fact]
        public void NullIsReturnedIfNoValidationAttributes()
        {
            Func<ImmutableList<ValidationError>, string, bool, string, ImmutableList<ValidationError>> fnRunValidation
                = (a, b, c,d ) => a;
            var generationResult = new ValidatorFunctionsGenerator()
                .GenerateValidationFunctions(new NoAttributesClass(),fnRunValidation);

            Assert.Null(generationResult);
        }

        

        [Fact]
        public void AttributesSupported()
        {
            VerifyAttributeIsSupported(new StringIsInSetAttribute(StringComparison.InvariantCulture,string.Empty));
            VerifyAttributeIsSupported(new StringIsNonNullable());
            VerifyAttributeIsSupported(new StringMaxLength(default));
            VerifyAttributeIsSupported(new DateMustBeFuture());
            VerifyAttributeIsSupported(new MinimumNumberAttribute(default));
            VerifyAttributeIsSupported(new EnumerableMinElementsAttribute(default));
        }

        private void VerifyAttributeIsSupported(object attribInstance)
        {
            var validatorAttrib = attribInstance as ValidatorAttributeBase;
            Assert.NotNull(validatorAttrib);
        }

    }

    class NoAttributesClass  { }

    class WithAttributesClass
    {

        [DateMustBeFuture]
        public DateTime FutureDateTime { get; set; }
    }
}