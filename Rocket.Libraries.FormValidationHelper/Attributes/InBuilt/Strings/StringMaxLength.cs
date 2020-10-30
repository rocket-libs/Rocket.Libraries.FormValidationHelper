namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
    {
        using System;
        [AttributeUsage (AttributeTargets.Property)]
        public class StringMaxLength : ValidatorAttributeBase
        {

            public StringMaxLength (int maxLength, string displayLabel) : base (displayLabel)
            {
                MaxLength = maxLength;
            }

            public static string MessageOnError => new StringMaxLength (default, string.Empty).ErrorMessage;

            public int MaxLength { get; private set; }

            public override string ErrorMessage => InsertDisplayLabel ($"Value is too long for {ValidatorAttributeBase.DisplayLabelPlaceholder}. Max length allowed is {MaxLength}");

                public override bool ValidationFailed (object value)
                {
                    var valueAsString = value == null ? string.Empty : value.ToString ();
                    var valueTooLong = valueAsString.Length > MaxLength;
                    return valueTooLong;
                }
            }
        }