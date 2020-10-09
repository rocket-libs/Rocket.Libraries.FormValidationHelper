using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringIsNonNullable : ValidatorAttributeBase
    {
        private readonly string displayLabel;

        public StringIsNonNullable(string displayLabel = "")
        {
            this.displayLabel = displayLabel;
        }

        public static string MessageOnError => new StringIsNonNullable().ErrorMessage;

        public override string ErrorMessage => string.IsNullOrEmpty(displayLabel) ? "A value is required for this field." : $"A value is required for field '{displayLabel}'";

        public override bool ValidationFailed(object value)
        {
            var valueAsString = value == null ? string.Empty : value.ToString();
            return string.IsNullOrEmpty(valueAsString);
        }
    }
}