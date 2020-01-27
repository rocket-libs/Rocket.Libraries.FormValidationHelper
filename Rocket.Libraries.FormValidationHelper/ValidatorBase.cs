using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Rocket.Libraries.FormValidationHelper
{
    public abstract class ValidatorBase<TEntity>
    {
        public abstract Task<ImmutableList<ValidationErrors>> ValidateAsync(TEntity unValidatedObject);

        protected ImmutableList<ValidationErrors> RunValidation(ImmutableList<ValidationErrors> errorsContainer, string fieldKey, bool isInvalid, string errorMessage)
        {
            Action<bool, string> throwExceptionIfTrue = (condition, errorDescription) =>
             {
                 if (condition)
                 {
                     throw new Exception(errorDescription);
                 }
             };

            throwExceptionIfTrue(string.IsNullOrEmpty(fieldKey), $"Field key is required for validators");
            throwExceptionIfTrue(string.IsNullOrEmpty(errorMessage), "Error message must be provided for validators");

            if (errorsContainer == null)
            {
                errorsContainer = ImmutableList<ValidationErrors>.Empty;
            }

            if (isInvalid)
            {
                errorsContainer = errorsContainer.Add(
                    new ValidationErrors
                    {
                        Errors = ImmutableList<string>.Empty.Add(errorMessage),
                        Key = fieldKey,
                    });
            }

            return errorsContainer;
        }
    }
}