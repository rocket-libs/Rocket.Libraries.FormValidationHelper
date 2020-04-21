using System;
using System.Linq;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsInSetAttribute : ValidatorAttributeBase
    {
        private readonly string[] set;

        public StringIsInSetAttribute(params string[] set)
        {
            this.set = set;
        }
        
        public override string ErrorMessage => "The value provide is not in the set of acceptable values. Note values are case-sensitive and camelCase is the default casing applied.";

        public override bool ValidationFailed(object value)
        {
            var valueAsString = value == null ? string.Empty : value.ToString();
            if(set == null || set.Length == 0)
            {
                return true;
            }
            else
            {
                return !set.Any(a => a.Equals(valueAsString, StringComparison.InvariantCulture));
            }
        }
    }
}