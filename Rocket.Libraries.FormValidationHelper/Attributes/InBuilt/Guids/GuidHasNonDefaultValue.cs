using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Guids
{
    public class GuidHasNonDefaultValue : ValidatorAttributeBase
    {
        public override string ErrorMessage => "A non null and non default GUID was expected for this field";

        public override bool ValidationFailed(object value)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }
            else
            {
                var parsesToGuid = Guid.TryParse(value.ToString(), out Guid guidValue);
                if(parsesToGuid && guidValue != default)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}