using System.Collections.Immutable;

namespace Rocket.Libraries.FormValidationHelper
{
    /// <summary>
    /// Contains information to describe a error that occured when a specific field was validated
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Gets or sets a value that uniquely identifies a field.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a list of errors that occured when field identified by <see cref="Key"/>
        /// </summary>
        public ImmutableList<string> Errors { get; set; }
    }
}