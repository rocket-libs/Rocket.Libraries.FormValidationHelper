using System;
using System.Collections.Immutable;
using System.Linq;
using Rocket.Libraries.FormValidationHelper;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Utility
{
    public static class ValidationErrorChecker
    {
        public static void ErrorReported<TModel>(ImmutableList<ValidationError> validationErrors,string fieldName, string errorMessage)
        {
            var fieldHasErrors = HasValidationErrorsForField<TModel>(validationErrors,fieldName);
            var key = ErrorKey<TModel>(fieldName);
            Assert.True(fieldHasErrors);
            Assert.Equal(errorMessage,validationErrors.Single(a => a.Key.Equals(key,StringComparison.InvariantCulture)).Errors.Single());
        }

        public static void ErrorNotReported<TModel>(ImmutableList<ValidationError> validationErrors,string fieldName, string errorMessage)
        {
            var fieldHasErrors = HasValidationErrorsForField<TModel>(validationErrors,fieldName);
            if (fieldHasErrors)
            {
                var key = ErrorKey<TModel>(fieldName);
                Assert.NotEqual(errorMessage, validationErrors.Single(a => a.Key.Equals(key, StringComparison.InvariantCulture)).Errors.Single());
            }
        }

        public static bool HasValidationErrorsForField<TModel>(ImmutableList<ValidationError> validationErrors, string fieldName)
        {
            var key = ErrorKey<TModel>(fieldName);
            return validationErrors.Any(a => a.Key.Equals(key));
        }

        private static string ErrorKey<TModel>(string fieldName)
        {
            var key = $"{typeof(TModel).Name}.{fieldName}";
            return key;
        }

    
    }
}