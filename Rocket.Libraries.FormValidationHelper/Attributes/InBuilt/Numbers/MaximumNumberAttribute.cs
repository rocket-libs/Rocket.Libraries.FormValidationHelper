using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    public class MaximumNumberAttribute : NumberSizeValidator
    {
        public MaximumNumberAttribute(double maximum, string displayLabel)
             : base(
                 maximum, 
                 $"Maximum value allowed for {ValidatorAttributeBase.DisplayLabelPlaceholder} is {maximum}",
                 NumberOperators.LessThan,
                 displayLabel)
        {
        }
    }
}