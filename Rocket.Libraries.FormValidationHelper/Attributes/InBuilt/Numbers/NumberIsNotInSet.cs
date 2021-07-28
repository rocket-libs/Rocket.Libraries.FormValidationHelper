using System.Collections.Immutable;
using System.Linq;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers
{
    public class NumberIsNotInSet : ValidatorAttributeBase
    {
        private readonly ImmutableHashSet<double> restrictedSet;
        private string stringValue;

        private double numberValue;
        public NumberIsNotInSet (string displayLabel, params double[] set) : base (displayLabel)
        {
            if (set == null)
            {
                restrictedSet = ImmutableHashSet<double>.Empty;
            }
            else
            {
                restrictedSet = set.GroupBy (a => a).Select (a => a.Key).ToImmutableHashSet ();
            }
        }

        public override string ErrorMessage => InsertDisplayLabel ($"The value '{stringValue}' is not allowed for field {ValidatorAttributeBase.DisplayLabelPlaceholder}.{DisallowedNumbersDescription}");
        
        public override bool ValidationFailed (object value)
        {
            if (value == default)
            {
                stringValue = "0";
            }
            else
            {
                stringValue = value.ToString ();
            }

            if (double.TryParse (stringValue, out numberValue) == false)
            {
                return true;
            }
            else
            {
                return restrictedSet.Any (restrictedNumber => restrictedNumber == numberValue);
            }
        }

        private string DisallowedNumbersDescription 
        {
            get
            {
                if (restrictedSet.Count > 1)
                {
                    var result = " (Restricted number(s):";
                    foreach (var specificNumber in restrictedSet)
                    {
                        result += $" '{specificNumber}',";
                    }
                    result = result.TrimEnd(',');
                    result += ") ";
                    return result;
                }
                else
                {
                    return string.Empty;
                }

            }
        }
    }
}