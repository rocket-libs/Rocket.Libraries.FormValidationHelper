using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateMustBeFuture : ValidatorAttributeBase
    {
        public static string MessageOnError => new DateMustBeFuture().ErrorMessage;
        
        public override string ErrorMessage => GetPrefixedWithDisplayLabelIfAvailable("Must be a future date.");

        

        public override bool ValidationFailed(object value)
        {
            var fieldValueAsDate = (DateTime)value;
            return fieldValueAsDate < DateTime.Now;
        }

        
    }
}