using System.Collections.Immutable;

namespace Rocket.Libraries.FormValidationHelper
{
    public class ValidationErrors
    {
        public string Key { get; set; }

        public ImmutableList<string> Errors { get; set; }
    }
}