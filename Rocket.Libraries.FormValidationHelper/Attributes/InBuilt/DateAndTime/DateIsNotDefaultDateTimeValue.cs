using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime
{
    public class DateIsNotDefaultDateTimeValue : ValidatorAttributeBase
    {
        public DateIsNotDefaultDateTimeValue(string displayLabel)
         : base(displayLabel)
        {
        }

        public override string ErrorMessage => $"A value must be provided for '{DisplayLabel}'";

        public override bool ValidationFailed(object value)
        {
            var fieldValueAsDate = (DateTime)value;
            return fieldValueAsDate == default;
        }
    }
}