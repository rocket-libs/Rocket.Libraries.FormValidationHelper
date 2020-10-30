using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime
{
    [AttributeUsage (AttributeTargets.Property)]
    public class DateMustTodayOrFuture : ValidatorAttributeBase
    {
        public override string ErrorMessage => GetPrefixedWithDisplayLabelIfAvailable("Must either be today, or a date in the future");

        public override bool ValidationFailed (object value)
        {
            var fieldValueAsDate = (DateTime) value;
            var now = DateTime.Now;
            var startOfDayToday = new DateTime (now.Year, now.Month, now.Day);
            return fieldValueAsDate < startOfDayToday;
        }
    }
}