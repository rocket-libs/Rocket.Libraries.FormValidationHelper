namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    using System;
    
    public class MinimumNumberAttribute : ValidatorAttributeBase
    {
        public MinimumNumberAttribute(double minimum)
        {
            Minimum = minimum;
        }

        public double Minimum { get; private set; }

        public override string ErrorMessage => $"Minimum value allowed for this field is {Minimum}";

        public override bool ValidationFailed(object value)
        {
            if(value == null)
            {
                value = 0;
            }

            var valueAsDouble = double.Parse(value.ToString());
            return valueAsDouble < Minimum;
        }
    }
}