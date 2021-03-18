using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Strings
{
    public class StringIsValidUrlOrDefault : ValidatorAttributeBase
    {
        private readonly bool acceptHTTP;
        private string valueAsString;

        /// <summary>
        /// Validation to ensure that a value is either blank or a valid url.
        /// By default only HTTPS urls are accepted as valid but you may set the <paramref name="acceptHTTP"/>
        /// value to true to accept the HTTP scheme as valid as well
        /// </summary>
        /// <param name="displayLabel">Gets or sets a user friendly name for the field being validated.</param>
        /// <param name="acceptHTTP">Controls whether or not the HTTP scheme is accepted as valid.</param>
        public StringIsValidUrlOrDefault(string displayLabel, bool acceptHTTP = false)
            : base(displayLabel)
        {
            this.acceptHTTP = acceptHTTP;
        }

        public override string ErrorMessage 
        {
            get
            {
                var errorMessage = InsertDisplayLabel ($"Value '{valueAsString}' for {ValidatorAttributeBase.DisplayLabelPlaceholder} is not a valid url.");
                if(acceptHTTP == false)
                {
                    errorMessage += $" Please note: Only the HTTPS scheme is accepted for values of this field.";
                }
                return errorMessage;
            }
        } 

        public override bool ValidationFailed(object value)
        {
            var valueAsString = value == default ? string.Empty : value.ToString();
            if(string.IsNullOrEmpty(valueAsString))
            {
                return false;
            }
            else
            {
                var uriResult = default(Uri);
                var acceptedSchemes = new HashSet<string>
                {
                    Uri.UriSchemeHttps
                };
                if(acceptHTTP)
                {
                    acceptedSchemes.Add(Uri.UriSchemeHttp);
                }
                var succeeded = Uri.TryCreate(valueAsString, UriKind.Absolute, out uriResult)
                    && acceptedSchemes.Any(a => a.Equals(uriResult.Scheme, StringComparison.InvariantCultureIgnoreCase));
                return succeeded == false;
            }
        }
    }
}