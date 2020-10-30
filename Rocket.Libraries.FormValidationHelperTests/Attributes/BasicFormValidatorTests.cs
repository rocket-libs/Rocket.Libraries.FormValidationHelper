using System.Linq;
using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Numbers;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes
{
    public class BasicFormValidatorTests
    {
        public const int MinimumId = 1;
        

        [Fact]
        public async Task ValidateAndPackReturnsErrorsWhenTheyOccur()
        {
            var model = new Model();
            using(var validator = new BasicFormValidator<Model>())
            {
                var result = await validator.ValidateAndPackAsync(model);
                Assert.Null(result.Entity);
                Assert.Equal(
                    new MinimumNumberAttribute(MinimumId,string.Empty).ErrorMessage,
                    result.ValidationErrors.Single().Errors.Single());
                Assert.Equal(
                    $"{nameof(Model)}.{nameof(Model.Id)}",
                    result.ValidationErrors.Single().Key
                );
            }
        }

        [Fact]
        public async Task ValidateAndPackReturnsSuccessObjectWhenThereAreNoErrors()
        {
            const int Id = 7;
            var model = new Model
            {
                Id = Id
            };
            using(var validator = new BasicFormValidator<Model>())
            {
                var result = await validator.ValidateAndPackAsync(model);
                Assert.Null(result.ValidationErrors);
                Assert.Equal(
                   Id,
                   result.Entity.Id
                );
            }
        }
    }

    class Model
    {
        [MinimumNumber(BasicFormValidatorTests.MinimumId,"")]
        public int Id { get; set; }
    }
}