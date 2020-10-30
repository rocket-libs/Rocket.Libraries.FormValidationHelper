using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    public class NumberEqualToAttribute : NumberSizeValidator
    {
        public NumberEqualToAttribute (double number, string displayLabel)
         : base (
             number, 
             $"Value of {ValidatorAttributeBase.DisplayLabelPlaceholder} must be equal to '{number}'",
             NumberOperators.EqualTo,
             displayLabel)
         {
             
         }
    }
}