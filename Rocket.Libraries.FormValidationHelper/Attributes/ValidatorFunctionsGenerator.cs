using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper;
using Rocket.Libraries.FormValidationHelper.Entities;

namespace Rocket.Libraries.FormValidationHelper.Attributes
{
    public class ValidatorFunctionsGenerator : IDisposable
    {
        public void Dispose()
        {
        }

        public ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>> GenerateValidationFunctions(
            object unValidatedObject,
            Func<ImmutableList<ValidationError>, string, bool, string, ImmutableList<ValidationError>> fnRunValidation)
        {
            var propertiesToSetup = unValidatedObject.GetType().GetProperties()
                .Where(
                        prop => prop.GetCustomAttributes(false)
                        .Any(attrib => IsInstanceOfIValidatorAttribute(attrib))
                    )
                .ToList();
            var noPropertiesToValidate = propertiesToSetup.Any() == false;
            if (noPropertiesToValidate)
            {
                return null;
            }
            else
            {
                var validationFunctions = ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>>.Empty;
                foreach (var targetProperty in propertiesToSetup)
                {
                    var funcs = Generate(unValidatedObject, targetProperty, fnRunValidation);
                    if (funcs.Count > 0)
                    {
                        validationFunctions = validationFunctions.AddRange(funcs);
                    }
                }
                return validationFunctions;
            }
        }

        private ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>> Generate(
            object unValidatedObject,
            PropertyInfo targetProperty,
            Func<ImmutableList<ValidationError>, string, bool, string, ImmutableList<ValidationError>> fnRunValidation)
        {
            ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>> fnValidators = ImmutableList<Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>>>.Empty;
            var manyValidatorAttributes = targetProperty.GetCustomAttributes(true)
                        .Where(attrib => IsInstanceOfIValidatorAttribute(attrib))
                        .Select(attrib => attrib as ValidatorAttributeBase)
                        .ToList();
            var value = targetProperty.GetValue(unValidatedObject);
            var key = $"{unValidatedObject.GetType().Name}.{targetProperty.Name}";
            foreach (var singleValidatorAttribute in manyValidatorAttributes)
            {
                Func<ImmutableList<ValidationError>, Task<ImmutableList<ValidationError>>> validation = async (validationErrors) =>
                {
                    await Task.Run(() => { });
                    return fnRunValidation(
                        validationErrors,
                        key,
                        singleValidatorAttribute.ValidationFailed(value),
                        PlaceholderReplacer.GetWithPlaceholdersReplaced(singleValidatorAttribute.ErrorMessage, unValidatedObject));
                };
                fnValidators = fnValidators.Add(validation);
            }
            return fnValidators;
        }

        private bool IsInstanceOfIValidatorAttribute(object attrib)
        {
            return typeof(ValidatorAttributeBase).IsAssignableFrom(attrib.GetType());
        }
    }
}