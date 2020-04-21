using System.Collections.Immutable;
using System.Threading.Tasks;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Enumerables
{
    public class EnumerableMinElementsTest
    {
        [Theory]
        [InlineData(null,true)]
        [InlineData(0,true)]
        [InlineData(1,false)]
        public async Task MinCollectionElements(int? elementsToAdd,bool expectError)
        {
            const int minLength = 1;
            var dummyClass = new MinCollectionElementsDummyClass();
            if(elementsToAdd.HasValue)
            {
                dummyClass.List_Min1 = ImmutableList<string>.Empty;
            }
            for (int i = 0; i < elementsToAdd.GetValueOrDefault(); i++)
            {
                dummyClass.List_Min1 = dummyClass.List_Min1.Add("blah");
            }
            
            using(var validator = new SharableValidator<MinCollectionElementsDummyClass>())
            {
                var validationErrors = await validator.ValidateAsync(dummyClass);
                if(expectError)
                {
                    ValidationErrorChecker.ErrorReported<MinCollectionElementsDummyClass>(
                        validationErrors,
                        nameof(MinCollectionElementsDummyClass.List_Min1),
                        new EnumerableMinElementsAttribute(minLength).ErrorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<MinCollectionElementsDummyClass>(
                        validationErrors,
                        nameof(MinCollectionElementsDummyClass.List_Min1),
                        new EnumerableMinElementsAttribute(minLength).ErrorMessage
                    );
                }
            }
        }

        class MinCollectionElementsDummyClass
        {
            [EnumerableMinElementsAttribute(1)]
            public ImmutableList<string> List_Min1 { get; set; }
        }
    }
}