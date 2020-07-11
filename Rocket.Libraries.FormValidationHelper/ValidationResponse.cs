using System.Collections.Immutable;

namespace Rocket.Libraries.FormValidationHelper
{
    /// <summary>
    /// Wrapper for information resulting from validation of data.
    /// </summary>
    /// <typeparam name="TEntity">The type of the data that will be returned by the save.</typeparam>
    public class ValidationResponse<TEntity>
    {
        /// <summary>
        /// Gets or returns data run through validation.
        /// </summary>
        public TEntity Entity { get; set; }

        /// <summary>
        /// Gets or sets a list of errors returned from validation.
        /// </summary>
        public ImmutableList<ValidationError> ValidationErrors { get; set; }

        /// <summary>
        /// Gets a value indicating whether validation resulted in errors.
        /// </summary>
        public bool HasErrors => ValidationErrors != null && ValidationErrors.Count > 0;
    }
}