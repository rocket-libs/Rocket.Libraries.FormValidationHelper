using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    public class EnumerableMaxElements : EnumerableElementsCount
    {
        public EnumerableMaxElements(int maxElements, string displayLabel)
         : base(
             maxElements, 
             $"Maximum number of elements expected for collection {ValidatorAttributeBase.DisplayLabelPlaceholder} is {maxElements}", 
             NumberOperators.LessThan, 
             displayLabel)
        {
        }
    }
}