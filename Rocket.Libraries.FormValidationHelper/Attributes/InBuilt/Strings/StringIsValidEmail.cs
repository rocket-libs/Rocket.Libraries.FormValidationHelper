using System.Net.Mail;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsValidEmail : ValidatorAttributeBase
    {
        private string userValue;
        public override string ErrorMessage => $"The value '{userValue}' does not look like a valid email address";

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