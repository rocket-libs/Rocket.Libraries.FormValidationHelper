namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsValidPhoneNumberWithCountryCodeOrDefault : ValidatorAttributeBase
    {
        private string valueAsString;

        public StringIsValidPhoneNumberWithCountryCodeOrDefault(string displayLabel)
         : base(displayLabel)
        {
        }

        public override string ErrorMessage => InsertDisplayLabel($"Value '{valueAsString}' for {ValidatorAttributeBase.DisplayLabelPlaceholder} is not a valid phone number. A leading '+' symbol as well as country code are expected");

        public override bool ValidationFailed (object value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                valueAsString = value.ToString ();
            }
            if (string.IsNullOrEmpty (valueAsString))
            {
                return false;
            }
            else
            {
                try
                {
                    var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance ();
                    var parsedPhoneNumber = phoneNumberUtil.Parse (valueAsString, null);
                    return parsedPhoneNumber == default;
                }
                catch
                {
                    return true;
                }
            }
        }
    }
}