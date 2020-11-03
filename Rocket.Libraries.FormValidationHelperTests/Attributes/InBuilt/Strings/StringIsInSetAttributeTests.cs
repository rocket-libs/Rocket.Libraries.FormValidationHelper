using System;
using System.Collections.Immutable;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class StringIsInSetAttributeTests
    {
        [Fact]
        public void EmptyStringsDoNotEvaluateToError ()
        {
            var emptyStringPermutations = ImmutableList<string>
                .Empty
                .Add (string.Empty)
                .Add ("")
                .Add (null);

            var stringIsInSetAttribute = new StringIsInSetAttribute (
                comparisonType: StringComparison.InvariantCultureIgnoreCase,
                "valid value"
            );

            foreach (var item in emptyStringPermutations)
            {
                Assert.False(stringIsInSetAttribute.ValidationFailed (item));
            }

        }
    }
}