using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringIsNonNullable : ValidatorAttributeBase
    {
        public static string MessageOnError => new StringIsNonNullable().ErrorMessage;
        
        public override string ErrorMessage => "A value is required for this field.";

        public override bool ValidationFailed(object value)
        {
            var valueAsString = value == null ? string.Empty : value.ToString();
            return string.IsNullOrEmpty(valueAsString);
        }
    }
}