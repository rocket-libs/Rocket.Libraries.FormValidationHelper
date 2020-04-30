using System;
using System.Linq;
using System.Text;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsInSetAttribute : ValidatorAttributeBase
    {
        private readonly string[] set;

        private string ComparisonTypeDescription
        {
            get
            {
                switch (ComparisonType)
                {
                    case StringComparison.CurrentCulture:
                    case StringComparison.InvariantCulture:
                    case StringComparison.Ordinal:
                        return "Note values are case-sensitive and camelCase is the default casing applied.";
                    case StringComparison.CurrentCultureIgnoreCase:
                    case StringComparison.InvariantCultureIgnoreCase:
                    case StringComparison.OrdinalIgnoreCase:
                        return "Values are compared in a case-insensitive manner.";
                    default:
                        throw new Exception ($"Unsupported string comparison mode: '${ComparisonType}'");
                }
            }
        }

        private string SupportedValues
        {
            get
            {
                var stringBuilder = new StringBuilder ();
                for (var i = 0; i < set.Length; i++)
                {
                    stringBuilder.Append ($" {set[i]}");
                    var isNotLastItem = i < set.Length - 1;
                    if (isNotLastItem)
                    {
                        stringBuilder.Append (", ");
                    }
                }
                return stringBuilder.ToString ();
            }
        }

        public StringIsInSetAttribute (StringComparison comparisonType, params string[] set)
        {
            this.set = set;
            ComparisonType = comparisonType;
        }

        public StringComparison ComparisonType { get; set; }

        public override string ErrorMessage => $"The value provide is not in the set of acceptable values: ({SupportedValues}). {ComparisonTypeDescription}";

        public override bool ValidationFailed (object value)
        {
            var valueAsString = value == null ? string.Empty : value.ToString ();
            if (set == null || set.Length == 0)
            {
                return true;
            }
            else
            {
                return !set.Any (a => a.Equals (valueAsString, ComparisonType));
            }
        }
    }
}