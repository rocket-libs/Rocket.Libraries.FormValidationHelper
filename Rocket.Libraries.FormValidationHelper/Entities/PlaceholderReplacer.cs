using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rocket.Libraries.FormValidationHelper.Entities
{
    internal static class PlaceholderReplacer
    {
        public static string GetWithPlaceholdersReplaced(string str, object entity)
        {
            var placeholdersMappings = new Dictionary<string, string>
            {
                { EntityPlaceholders.DisplayLabel, nameof(ValidationEntityMetadataAttribute.DisplayLabel) }
            };

            var entityMetadata = entity.GetType()
                .GetCustomAttributes()
                .FirstOrDefault(a => a.GetType() == typeof(ValidationEntityMetadataAttribute));

            if (entityMetadata != default)
            {
                foreach (var specificPlaceHolder in placeholdersMappings)
                {
                    var replacementValue = entityMetadata.GetType().GetProperty(specificPlaceHolder.Value)
                           .GetValue(entityMetadata);
                    if (replacementValue != null)
                    {
                        str = str.Replace(specificPlaceHolder.Key, replacementValue.ToString());
                    }
                }
            }
            return str;
        }
    }
}