using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Rocket.Libraries.FormValidationHelper.Attributes
{
    public class BasicFormValidator<TObject> : FormValidationBase<TObject>
    {
        public BasicFormValidator()
        {
        }

        public async Task<ValidatedSaveResponse<TWrappedObject>> ValidateAndWrapAsync<TWrappedObject>(TObject unValidatedObject)
        {
            return new ValidatedSaveResponse<TWrappedObject>
            {
                ValidationErrors = await ValidateAsync(unValidatedObject)
            };
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