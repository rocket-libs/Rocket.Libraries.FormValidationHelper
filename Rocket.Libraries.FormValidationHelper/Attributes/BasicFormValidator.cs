using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Rocket.Libraries.FormValidationHelper.Attributes
{
    public class BasicFormValidator<TObject> : FormValidationBase<TObject>
    {
        public BasicFormValidator()
        {
        }

        public async Task<ValidationResponse<TObject>> ValidateAndPackAsync(TObject unValidatedObject)
        {
            var validationErrors = await ValidateAsync(unValidatedObject);
            if (validationErrors != null && validationErrors.Count > 0)
            {
                return new ValidationResponse<TObject>
                {
                    ValidationErrors = validationErrors
                };
            }
            else
            {
                return new ValidationResponse<TObject>
                {
                    Entity = unValidatedObject
                };
            }
        }

        public override async Task<ImmutableList<ValidationError>> ValidateAsync(TObject unValidatedObject)
        {
            return await ValidateProxyAsync(
                unValidatedObject,
                OnNonNullValidationsAsync
            );
        }

        protected override async Task<ImmutableList<ValidationError>> OnNonNullValidationsAsync(TObject unValidatedObject, ImmutableList<ValidationError> validationErrors)
        {
            await Task.Run(() => { });
            return validationErrors;
        }
    }
}