namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Objects
{
    public class ObjectIsNotNull : ValidatorAttributeBase
    {
        private readonly string displayLabel;

        public ObjectIsNotNull(string displayLabel = "")
        {
            this.displayLabel = string.IsNullOrEmpty(displayLabel) ? "item" : displayLabel;
        }
        public override string ErrorMessage => $"No value was provided for {displayLabel}";

        public override bool ValidationFailed (object value)
        {
            return value == null;
        }
    }
}