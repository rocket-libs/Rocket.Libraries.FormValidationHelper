using System;
using System.Collections.Generic;
using System.Text;

namespace Rocket.Libraries.FormValidationHelper.Entities
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ValidationEntityMetadataAttribute : Attribute
    {
        public ValidationEntityMetadataAttribute(
            string displayLabel)
        {
            DisplayLabel = displayLabel;
        }

        public string DisplayLabel { get; }
    }
}