using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Strings
{
    public class ValidatableStringObject
    {
        [StringIsNonNullable]
        public string NonNullableStringValue { get; set; }

        public string NullableStringValue { get; set; }

        [StringMaxLength(4)]
        public string MaxLength_4_Value { get; set; }
    }
}