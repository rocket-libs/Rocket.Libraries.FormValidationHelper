using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Rocket.Libraries.FormValidationHelper
{
    public interface IValidationResponseHelper
    {
        ValidationResponse<TResult> TypedError<TResult>(string errorMessage, string key);
        ValidationResponse<TResult> SuccessValue<TResult>(TResult result);
        Task<ValidationResponse<TResult>> RouteResponseTypedAsync<TSource, TResult>(ValidationResponse<TSource> validationResponse, Func<TSource, Task<ValidationResponse<TResult>>> ifNoErrorCallback);
        ValidationResponse<TResult> Translate<TSource, TResult>(ValidationResponse<TSource> validationResponse);
        Task<ValidationResponse<TResult>> SuccessValueAsync<TResult>(TResult result);
        ValidationResponse<object> Success();
        Task<ValidationResponse<TResult>> RouteResponseAsync<TResult>(ValidationResponse<object> validationResponse, Func<Task<ValidationResponse<TResult>>> ifNoErrorCallback);
        ValidationResponse<object> Error(string errorMessage, string key);
    }

    public class ValidationResponseHelper : IValidationResponseHelper
    {
        public ValidationResponse<TResult> SuccessValue<TResult>(TResult result)
        {
            return new ValidationResponse<TResult>
            {
                Entity = result
            };
        }

        public ValidationResponse<object> Success()
        {
            return SuccessValue<object>(default(object));
        }


        public async Task<ValidationResponse<TResult>> SuccessValueAsync<TResult>(TResult result)
        {
            return await Task.Run(() => SuccessValue(result));
        }

        public ValidationResponse<TResult> TypedError<TResult>(string errorMessage, string key)
        {
            return new ValidationResponse<TResult>
            {
                ValidationErrors = ErrorList(errorMessage, key)
            };
        }
        
        public ValidationResponse<object> Error(string errorMessage, string key )
        {
            return TypedError<object>(errorMessage, key);
        }

        public ImmutableList<ValidationError> ErrorList(string errorMessage, string key)
        {
            return ImmutableList<ValidationError>.Empty.Add(
                    new ValidationError
                    {
                        Errors = ImmutableList<string>.Empty.Add(errorMessage),
                        Key = key
                    }
                );
        }

        public ValidationResponse<TResult> Translate<TSource,TResult>(ValidationResponse<TSource> validationResponse)
        {
            return new ValidationResponse<TResult>
            {
                ValidationErrors = validationResponse.ValidationErrors
            };
        }

        public async Task<ValidationResponse<TResult>> RouteResponseAsync<TResult>(ValidationResponse<object> validationResponse, Func<Task<ValidationResponse<TResult>>> ifNoErrorCallback)
        {
            return await RouteResponseTypedAsync<object, TResult>(validationResponse, async (_) => await ifNoErrorCallback());
        }
        public async Task<ValidationResponse<TResult>> RouteResponseTypedAsync<TSource, TResult>(ValidationResponse<TSource> validationResponse, Func<TSource, Task<ValidationResponse<TResult>>> ifNoErrorCallback)
        {
            if(validationResponse.HasErrors)
            {
                return Translate<TSource, TResult>(validationResponse);
            }
            else
            {
                return await ifNoErrorCallback(validationResponse.Entity);
            }
        }
    }
}