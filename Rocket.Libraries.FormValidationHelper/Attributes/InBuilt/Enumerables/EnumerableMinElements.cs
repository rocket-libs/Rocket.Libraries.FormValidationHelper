using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    public class EnumerableMinElementsAttribute : EnumerableElementsCount
    {
        public EnumerableMinElementsAttribute(int minElements)
         : base(
             minElements, 
             $"Minimum number of elements expected in this is {minElements}", 
             NumberOperators.GreaterThan)
        {
        }
    }
}