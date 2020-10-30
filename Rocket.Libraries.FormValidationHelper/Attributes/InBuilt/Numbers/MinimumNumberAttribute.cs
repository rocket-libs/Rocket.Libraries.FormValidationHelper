namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    using Rocket.Libraries.FormValidationHelper.Shared;

    public class MinimumNumberAttribute : NumberSizeValidator
    {
        public MinimumNumberAttribute(double minimum,string displayLabel)
            : base(
                minimum,
                $"Minimum value allowed for {ValidatorAttributeBase.DisplayLabelPlaceholder} is {minimum}",
                NumberOperators.GreaterThan,
                displayLabel)
        {
            
        }
    }
}