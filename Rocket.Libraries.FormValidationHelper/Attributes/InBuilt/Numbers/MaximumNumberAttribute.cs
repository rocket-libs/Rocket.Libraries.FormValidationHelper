using Rocket.Libraries.FormValidationHelper.Shared;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    public class MaximumNumberAttribute : NumberSizeValidator
    {
        public MaximumNumberAttribute(double maximum)
             : base(maximum, $"Maximum value allowed for this field is {maximum}",NumberOperators.LessThan)
        {
        }
    }
}