using System;

namespace Rocket.Libraries.FormValidationHelper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidatorAttributeBase : Attribute
    {
        public abstract string ErrorMessage { get; }

        public abstract bool ValidationFailed(object value);
    }
}