using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables
{
    public abstract class EnumerableElementsCount : ValidatorAttributeBase
    {
        private readonly int minElements;
        private readonly int targetNumber;
        private readonly string operation;

        public EnumerableElementsCount (int targetNumber, string errorMessage, string operation)
        {
            this.targetNumber = targetNumber;
            ErrorMessage = errorMessage;
            this.operation = operation;
        }

        public override string ErrorMessage { get; }

        public override bool ValidationFailed (object value)
        {
            IEnumerable enumerable = value as IEnumerable;
            if (enumerable == null)
            {
                enumerable = new List<object> ();
            }

            var objEnumerable = enumerable.Cast<object> ();
            var enumerableCalculator = new EnumerableCalculator (
                targetNumber,
                ErrorMessage,
                operation);
            return enumerableCalculator.ValidationFailed (objEnumerable.Count ());
        }
    }
}