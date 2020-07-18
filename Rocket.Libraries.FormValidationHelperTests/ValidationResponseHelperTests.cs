using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests
{
    public class ValidationResponseHelperTests
    {
        [Fact]
        public void GenericSuccessReturnWorksCorrectly ()
        {
            var result = new ValidationResponseHelper ().Success ();
            Assert.Null (result.Entity);
            Assert.Null (result.ValidationErrors);
        }

        [Fact]
        public async Task SuccessValueAsyncWorksCorrectly ()
        {
            const byte entityValue = 9;
            var result = await new ValidationResponseHelper ().SuccessValueAsync (entityValue);
            Assert.Equal (entityValue, result.Entity);
            Assert.Null (result.ValidationErrors);
        }

        [Fact]
        public async Task RouteResponseAsyncWithTypeWorks ()
        {
            const int entityValue = 3;
            const int doubledValue = entityValue * 2;

            var validationResponse = new ValidationResponse<int>
            {
                Entity = entityValue
            };

            Func<int, Task<ValidationResponse<int>>> doubler = async (v) => await new ValidationResponseHelper ().SuccessValueAsync (v * 2);

            var result = await new ValidationResponseHelper ().RouteResponseAsync (validationResponse, doubler);

            Assert.Equal (doubledValue, result.Entity);
            Assert.Null (result.ValidationErrors);
        }

        [Fact]
        public void SuccessReturnerWorksCorrectly ()
        {
            const byte expectedResult = 235;
            var result = new ValidationResponseHelper ().SuccessValue (expectedResult);
            Assert.Equal (expectedResult, result.Entity);
        }

        [Fact]
        public void ErrorReturnerWorksCorrectly ()
        {
            const string expectedResult = "  This is an error message with      lots of spaces and stuff ";
            const string key = "XUnitTestErrorKey";
            var result = new ValidationResponseHelper ().TypedError<int> (expectedResult, key);
            var actualError = result.ValidationErrors.Single (a => a.Key.Equals (key, StringComparison.InvariantCulture));
            Assert.Equal (expectedResult, actualError.Errors.Single ());
        }

        [Fact]
        public void TranslationWorksCorrectly ()
        {
            const byte value = 235;
            var initialResponse = new ValidationResponse<byte>
            {
                Entity = value
            };

            var translatedResult = new ValidationResponseHelper ().Translate<byte, int> (initialResponse);

            Assert.Equal (typeof (int), translatedResult.Entity.GetType ());
        }

        [Theory]
        [InlineData (true)]
        [InlineData (false)]
        public async Task RoutingTypedWhenInErrorWorksCorrectly (bool hasError)
        {
            const string errorText = "This is the error text";
            const string errorKey = "XUnitErrorKey";
            const string successText = "There Was No Error";

            Func<string, Task<ValidationResponse<string>>> validDataCallback = (s) => Task.Run (() => new ValidationResponse<string>
            {
                Entity = s
            });

            var validationErrors = ImmutableList<ValidationError>.Empty.Add (new ValidationError
            {
                Errors = ImmutableList<string>.Empty.Add (errorText),
                    Key = errorKey
            });

            var response = new ValidationResponse<string>
            {
                Entity = hasError ? default : successText,
                ValidationErrors = hasError ? validationErrors : default
            };

            var result = await new ValidationResponseHelper ().RouteResponseTypedAsync<string, string> (
                    response,
                    async (validString) => await validDataCallback (validString));

            if (hasError)
            {
                Assert.Equal (errorKey, result.ValidationErrors.Single ().Key);
                Assert.Equal (errorText, result.ValidationErrors.Single ().Errors.Single ());
            }
            else
            {
                Assert.Equal (successText, result.Entity);
            }

        }

        [Theory]
        [InlineData (true)]
        [InlineData (false)]
        public async Task RoutingBasicWorksCorrectly (bool hasError)
        {
            const string errorText = "This is the error text";
            const string errorKey = "XUnitErrorKey";
            const string successText = "There Was No Error";

            Func<string, Task<ValidationResponse<string>>> validDataCallback = (s) => Task.Run (() => new ValidationResponse<string>
            {
                Entity = s
            });

            var validationErrors = ImmutableList<ValidationError>.Empty.Add (new ValidationError
            {
                Errors = ImmutableList<string>.Empty.Add (errorText),
                    Key = errorKey
            });

            var response = new ValidationResponse<object>
            {
                ValidationErrors = hasError ? validationErrors : default
            };

            var result = await new ValidationResponseHelper ().RouteResponseAsync<string> (
                response,
                async () => await validDataCallback (successText));

            if (hasError)
            {
                Assert.Equal (errorKey, result.ValidationErrors.Single ().Key);
                Assert.Equal (errorText, result.ValidationErrors.Single ().Errors.Single ());
            }
            else
            {
                Assert.Equal (successText, result.Entity);
            }

        }
    }
}