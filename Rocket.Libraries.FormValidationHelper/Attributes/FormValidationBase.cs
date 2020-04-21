namespace Rocket.Libraries.FormValidationHelper.Attributes
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Rocket.Libraries.FormValidationHelper;
    using Rocket.Libraries.PropertyNameResolver;

    public abstract class FormValidationBase<TObject> : ValidatorBase<TObject>, IDisposable
    {
        public const string MaxLengthPlaceholder = "{{max-length}}";
        public const string ErrorMessageObjectIsNull = "No data was provided. Nothing to save";
        public const string ErrorMessageRequiredFieldEmpty = "This field cannot be empty";

        public const string ErrorMessageStringNotInSubset = "The value not in list of valid values for field";
        

        private ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>> attributeValidations = ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>>.Empty;

        public static string ErrorMessageMaxLengthExceeded => $"Max length '{MaxLengthPlaceholder}' has been exceeded.";
        

        public void Dispose()
        {            
        }
        
        [Obsolete("Use attribute validation instead")]
        protected ImmutableList<ValidationError> ValidateMaxLength<TField>(TObject unValidatedObject, ImmutableList<ValidationError> validationErrors, Expression<Func<TObject, TField>> fieldKeyDescriber, int length)
        {
            var fieldValue = GetFieldValue(unValidatedObject, fieldKeyDescriber);
            if(fieldValue == null)
            {
                return validationErrors;
            }
            else
            {
                return RunValidation(
                    validationErrors,
                    fieldKeyDescriber,
                    fieldValue.ToString().Length > length,
                    ErrorMessageMaxLengthExceeded.Replace(MaxLengthPlaceholder, length.ToString())
                );
            }
        }



        [Obsolete("Use Attribute validation instead")]
        protected ImmutableList<ValidationError> ValidateStringIsNotInSubset<TField>(TObject unValidatedObject, ImmutableList<ValidationError> validationErrors, Expression<Func<TObject, TField>> fieldKeyDescriber, params string[] fullset)
        {
            var fieldValue = GetFieldValue(unValidatedObject, fieldKeyDescriber);
            var fieldValueAsString = fieldValue == null ? string.Empty : fieldValue.ToString();
            return RunValidation(
                validationErrors,
                fieldKeyDescriber,
                !fullset.Any(a => a.Equals(fieldValueAsString, StringComparison.InvariantCulture)),
                ErrorMessageStringNotInSubset);
        }

        [Obsolete("Use Attribute validation instead")]
        protected ImmutableList<ValidationError> ValidateStringNotEmpty<TField>(TObject unValidatedObject, ImmutableList<ValidationError> validationErrors, Expression<Func<TObject, TField>> fieldKeyDescriber)
        {
            var fieldValue = GetFieldValue(unValidatedObject, fieldKeyDescriber);
            var fieldValueAsString = fieldValue == null ? string.Empty : fieldValue.ToString();
            return RunValidation(
                validationErrors,
                fieldKeyDescriber,
                string.IsNullOrEmpty(fieldValueAsString),
                ErrorMessageRequiredFieldEmpty);
        }
        protected async Task<ImmutableList<ValidationError>> ValidateProxyAsync(TObject unValidatedObject, Func<TObject, ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>> onNonNullObjectValidations)
        {
            var objectIsNull = unValidatedObject == null;
            var validationErrors = ImmutableList<ValidationError>.Empty;
            validationErrors = RunValidation(
                validationErrors,
                typeof(TObject).Name,
                objectIsNull,
                ErrorMessageObjectIsNull);
            if(objectIsNull) 
            {
                return validationErrors;
            }
            else 
            {
                SetupAttributeValidation(unValidatedObject);
                foreach (var fnValidator in attributeValidations)
                {
                    validationErrors = await fnValidator(validationErrors);
                }
                return await onNonNullObjectValidations(unValidatedObject, validationErrors);
            }
        }

        protected abstract Task<ImmutableList<ValidationError>> OnNonNullValidationsAsync(TObject unValidatedObject, ImmutableList<ValidationError> validationErrors);

        private object GetFieldValue<TField>(TObject unValidatedObject, Expression<Func<TObject, TField>> fieldKeyDescriber)
        {
            var objectIsNull = unValidatedObject == null;
            if(objectIsNull)
            {
                return null;
            }
            else
            {
                var fieldName = new PropertyNameResolver().Resolve(fieldKeyDescriber);
                return unValidatedObject.GetType()
                    .GetProperty(fieldName)
                    .GetValue(unValidatedObject);
            }
        }

        private void SetupAttributeValidation(TObject unValidatedObject)
        {
            using(var validatorGenerator = new ValidatorFunctionsGenerator())
            {
                var validationFunctions = validatorGenerator.GenerateValidationFunctions(
                    unValidatedObject,
                    RunValidation
                );

                if(validationFunctions != null)
                {
                    attributeValidations = attributeValidations.AddRange(validationFunctions);
                }
            }
        }
    }
}