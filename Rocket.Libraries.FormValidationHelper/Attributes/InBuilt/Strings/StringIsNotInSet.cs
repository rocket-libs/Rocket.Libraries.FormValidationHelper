using System;
using System.Linq;
using System.Text;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsNotInSet : ValidatorAttributeBase
    {
        private readonly StringComparison comparisonType;
        private readonly string[] exclusionStrings;

        private string valueForValidation = string.Empty;

        private string OtherInvalidStrings
        {
            get
            {
                
                if(exclusionStrings == null || exclusionStrings.Length == 0)
                {
                    return string.Empty;
                }
                else
                {
                    var otherStrings = exclusionStrings
                       .Where(a => !a.Equals(valueForValidation, comparisonType))
                       .ToArray();

                    if(otherStrings.Length == 0)
                    {
                        return "";
                    }
                    else
                    {
                        var stringBuilder = new StringBuilder(" Other disallowed value(s): ");

                        for (int i = 0; i < otherStrings.Length; i++)
                        {
                            if (otherStrings[i] != valueForValidation)
                            {
                                stringBuilder.Append($"'{otherStrings[i]}'");
                                var isSecondLastItem = i == otherStrings.Length - 2;
                                var isLastItem = i == otherStrings.Length - 1;
                                if (isSecondLastItem)
                                {
                                    stringBuilder.Append(" and ");
                                }
                                else if(isLastItem)
                                {
                                    stringBuilder.Append(".");
                                }
                                else
                                {
                                    stringBuilder.Append(", ");
                                }
                            }
                        }

                        return stringBuilder.ToString();
                    }
                }
            }
        }

        public StringIsNotInSet(StringComparison comparisonType, params string[] exclusionSet)
        {
            this.comparisonType = comparisonType;
            this.exclusionStrings = exclusionSet;
        }

        public override string ErrorMessage => $"Value '{valueForValidation}' is not allowed for this field.{OtherInvalidStrings}";

        public override bool ValidationFailed(object value)
        {
            if(value == null || exclusionStrings == null || exclusionStrings.Length == 0)
            {
                return false;
            }
            valueForValidation = value.ToString();
            return exclusionStrings.Any(a => a.Equals(valueForValidation, comparisonType));
        }

        
    }
}