using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Rocket.Libraries.PropertyNameResolver;

namespace Rocket.Libraries.FormValidationHelper
{
    public abstract class ValidatorBase<TEntity>
    {
        public abstract Task<ImmutableList<ValidationError>> ValidateAsync(TEntity unValidatedObject);

        protected ImmutableList<ValidationError> RunValidation<TPropertyType>(ImmutableList<ValidationError> errorsContainer, Expression<Func<TEntity, TPropertyType>> fieldKeyDescriber, bool isInvalid, string errorMessage)
        {
            var propertyName = new TypedPropertyNamedResolver<TEntity>().Resolve(fieldKeyDescriber);
            var qualifiedPropertyName = $"{typeof(TEntity).Name}.{propertyName}";
            return RunValidation(errorsContainer, qualifiedPropertyName, isInvalid, errorMessage);
        }

        protected ImmutableList<ValidationError> RunValidation(ImmutableList<ValidationError> errorsContainer, string fieldKey, bool isInvalid, string errorMessage)
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
                errorsContainer = ImmutableList<ValidationError>.Empty;
            }

            if (isInvalid)
            {
                errorsContainer = errorsContainer.Add(
                    new ValidationError
                    {
                        Errors = ImmutableList<string>.Empty.Add(errorMessage),
                        Key = fieldKey,
                    });
            }

            return errorsContainer;
        }
    }
}