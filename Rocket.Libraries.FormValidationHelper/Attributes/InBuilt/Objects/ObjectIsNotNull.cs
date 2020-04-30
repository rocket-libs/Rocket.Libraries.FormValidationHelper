namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Objects
{
    public class ObjectIsNotNull : ValidatorAttributeBase
    {
        public override string ErrorMessage => "Item was not expected to be empty";

        public override bool ValidationFailed (object value)
        {
            return value == null;
        }
    }
}