namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    using System;
    [AttributeUsage(AttributeTargets.Property)]    
    public class StringMaxLength : ValidatorAttributeBase
    {

        public StringMaxLength(int maxLength)
        {
            MaxLength = maxLength;
        }

        public static string MessageOnError => new StringMaxLength(default).ErrorMessage;

        public int MaxLength { get; private set; }


        public override string ErrorMessage => "Value is too long for this field";


        public override bool ValidationFailed(object value)
        {
            var valueAsString = value == null ? string.Empty : value.ToString();
            var valueTooLong = valueAsString.Length > MaxLength;
            return valueTooLong;
        }
    }
}