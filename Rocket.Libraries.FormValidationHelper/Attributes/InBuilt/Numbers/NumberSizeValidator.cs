using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    public class NumberSizeValidator : ValidatorAttributeBase
    {
        private readonly string operationSpecificErrorMessage;
        private readonly string operation;

        internal NumberSizeValidator (double targetNumber, string errorMessage, string operation)
        {
            TargetNumber = targetNumber;
            ErrorMessage = errorMessage;
            this.operation = operation;
        }

        public double TargetNumber { get; private set; }

        public override string ErrorMessage { get; }

        public override bool ValidationFailed (object value)
        {
            if (value == null)
            {
                value = 0;
            }

            var valueAsDouble = double.Parse (value.ToString ());
            switch (operation)
            {
                case NumberOperators.GreaterThan:
                    return valueAsDouble < TargetNumber;
                case NumberOperators.EqualTo:
                    return valueAsDouble != TargetNumber;
                default:
                    throw new Exception ($"Unsupported number operation '{operation}'");
            }

        }
    }
}