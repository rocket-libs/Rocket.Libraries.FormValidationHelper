using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Rocket.Libraries.FormValidationHelper
{
    /// <summary>
    /// Container for information about validation of data intended for saving, as well as optionally the <see cref="SaveResult"/> of the saving operation
    /// </summary>
    /// <typeparam name="TEntity">The type of the data that will be returned by the save</typeparam>
    public class ValidatedSaveResponse<TEntity>
    {
        /// <summary>
        /// Gets or returns data returned from the save operation
        /// </summary>
        public TEntity SaveResult { get; set; }

        /// <summary>
        /// Gets or sets a list of errors returned from validation of data before saving
        /// </summary>
        public ImmutableList<ValidationError> ValidationErrors { get; set; }

        /// <summary>
        /// Gets a value indicating whether validation resulted in errors
        /// </summary>
        public bool HasErrors => ValidationErrors != null && ValidationErrors.Count > 0;
    }
}