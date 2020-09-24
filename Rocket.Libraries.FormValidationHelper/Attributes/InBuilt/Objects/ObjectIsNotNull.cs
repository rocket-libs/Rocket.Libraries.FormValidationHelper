namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Objects
{
    public class ObjectIsNotNull : ValidatorAttributeBase
    {
        private readonly string displayLabel;

        public ObjectIsNotNull(string displayLabel = "Item")
        {
            this.displayLabel = displayLabel;
        }
        public override string ErrorMessage => $"{displayLabel} was not expected to be empty";

        public override bool ValidationFailed (object value)
        {
            return value == null;
        }
    }
}