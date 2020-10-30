using System.Net.Mail;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    /// <summary>
    /// String.Empty, "" or null value are not reported as invalid emails.
    /// Use inconjunction with <see ref="StringIsNonNullable" /> for required fields.
    /// </summary>
    public class StringIsValidEmailOrDefault : ValidatorAttributeBase
    {
        private string userValue;

        public StringIsValidEmailOrDefault(string displayLabel) : base(displayLabel)
        {
        }

        public override string ErrorMessage => InsertDisplayLabel($"The value '{userValue}' for field {ValidatorAttributeBase.DisplayLabelPlaceholder} does not look like a valid email address");

        public override bool ValidationFailed(object value)
        {
            if(value == default)
            {
                userValue = string.Empty;
            }
            else
            {
                userValue = value.ToString();
            }
            if(string.IsNullOrEmpty(userValue))
            {
                return false;
            }
            else
            {
                try
                {
                    var addr = new MailAddress(userValue);
                    return addr.Address != userValue;
                }
                catch
                {
                    return true;
                }
            }
        }
    }
}