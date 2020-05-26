using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    public class EnumerableMaxElements : EnumerableElementsCount
    {
        public EnumerableMaxElements(int maxElements)
         : base(
             maxElements, 
             $"Maximum number of elements expected in this is {maxElements}", 
             NumberOperators.LessThan)
        {
        }
    }
}