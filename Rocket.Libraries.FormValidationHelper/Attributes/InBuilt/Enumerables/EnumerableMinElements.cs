using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    public class EnumerableMinElementsAttribute : EnumerableElementsCount
    {
        public EnumerableMinElementsAttribute(int minElements, string displayLabel)
         : base(
             minElements, 
             $"Minimum number of elements expected in collection {ValidatorAttributeBase.DisplayLabelPlaceholder} is {minElements}", 
             NumberOperators.GreaterThan, 
             displayLabel)
        {
        }
    }
}