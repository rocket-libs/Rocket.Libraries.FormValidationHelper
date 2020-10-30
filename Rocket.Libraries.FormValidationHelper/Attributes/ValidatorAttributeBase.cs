using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes
    {
        [AttributeUsage (AttributeTargets.Property)]
        public abstract class ValidatorAttributeBase : Attribute
            {
                public const string DisplayLabelPlaceholder = "{{display-label}}";

                public ValidatorAttributeBase (string displayLabel = "")
                {
                    DisplayLabel = displayLabel;
                }

                protected string InsertDisplayLabel (string errorMessage)
                    {
                        return errorMessage.Replace (DisplayLabelPlaceholder, $"'{DisplayLabel}'");
        }

        protected string GetPrefixedWithDisplayLabelIfAvailable (string errorMessage)
        {
            errorMessage = InsertDisplayLabel (errorMessage);
            if (string.IsNullOrEmpty (DisplayLabel))
            {
                return errorMessage;
            }
            else
            {
                return $"{DisplayLabel} {errorMessage.ToLowerInvariant ()}";
            }
        }

        public abstract string ErrorMessage { get; }
        protected string DisplayLabel { get; }

        public abstract bool ValidationFailed (object value);
    }
}