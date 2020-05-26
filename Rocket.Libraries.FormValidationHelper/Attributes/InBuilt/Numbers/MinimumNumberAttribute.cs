namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    using System;
    using Rocket.Libraries.FormValidationHelper.Shared;

    public class MinimumNumberAttribute : NumberSizeValidator
    {
        public MinimumNumberAttribute(double minimum)
            : base(minimum,$"Minimum value allowed for this field is {minimum}",NumberOperators.GreaterThan)
        {
            
        }
    }
}