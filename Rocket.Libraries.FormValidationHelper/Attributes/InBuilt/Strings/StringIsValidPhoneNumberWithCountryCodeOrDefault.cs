namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsValidPhoneNumberWithCountryCodeOrDefault : ValidatorAttributeBase
    {
        private readonly bool requireLeadingPlusSign;
        private string valueAsString;

        /// <param name="displayLabel">A user friendly name of the field being validated.</param>
        /// <param name="requireLeadingPlusSign">
        /// If phone numbers must start with the 'plus' sign, set this to true.
        /// Set it to false, if it is ok for phone numbers to start with just the country code
        /// without the leading 'plus' sign.
        /// </param>
        /// <returns></returns>
        public StringIsValidPhoneNumberWithCountryCodeOrDefault (
            string displayLabel,
            bool requireLeadingPlusSign)
             : base (displayLabel)
        {
            this.requireLeadingPlusSign = requireLeadingPlusSign;
        }

        public override string ErrorMessage => InsertDisplayLabel ($"Value '{valueAsString}' for {ValidatorAttributeBase.DisplayLabelPlaceholder} is not a valid phone number. A leading '+' symbol as well as country code are expected");

        public override bool ValidationFailed (object value)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                valueAsString = value.ToString ();
                if (requireLeadingPlusSign == false && !valueAsString.StartsWith ("+"))
                {
                    // 'Plus' sign is always required, so the 'requireLeadingPlusSign' flag in the constructor
                    // is simply for user convenience, and we'll always add the sign in.
                    // Strings are immutable, this won't change the original value on the user's object
                    // but it will allow us perform the validation.
                    valueAsString = $"+{valueAsString}";
                }
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