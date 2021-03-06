using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    class EnumerableCalculator : NumberSizeValidator
    {
        public EnumerableCalculator(double targetNumber, string errorMessage, string operation, string displayLabel)
         : base(targetNumber, errorMessage, operation, displayLabel)
        {
        }
    }
}