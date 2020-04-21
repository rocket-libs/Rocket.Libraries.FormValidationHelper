using System.Collections;
using System.Linq;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    public class EnumerableMinElementsAttribute : ValidatorAttributeBase
    {
        private readonly int minElements;

        public EnumerableMinElementsAttribute(int minElements)
        {
            this.minElements = minElements;
        }

        public override string ErrorMessage => $"Minimum number of elements expected in this is {minElements}";

        public override bool ValidationFailed(object value)
        {
            IEnumerable enumerable = value as IEnumerable;
            if(enumerable == null)
            {
                return true;
            }
            else
            {
                var objEnumerable = enumerable.Cast<object>();
                return objEnumerable.Count() < minElements;
            }
        }
    }
}