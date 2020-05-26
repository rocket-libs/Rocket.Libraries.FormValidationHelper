using System.Collections.Immutable;
using System.Threading.Tasks;
using Rocket.Libraries.FormValidationHelper.Attributes;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.Enumerables;
using Rocket.Libraries.FormValidationHelperTests.Utility;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.Enumerables
{
    public class EnumerableMaxElementsTest
    {
        [Theory]
        [InlineData(null,false)]
        [InlineData(0,false)]
        [InlineData(1,false)]
        [InlineData(2,true)]
        public async Task MinCollectionElements(int? elementsToAdd,bool expectError)
        {
            const int maxLength = 1;
            var dummyClass = new MaxCollectionElementsDummyClass();
            if(elementsToAdd.HasValue)
            {
                dummyClass.List_Max1 = ImmutableList<string>.Empty;
            }
            for (int i = 0; i < elementsToAdd.GetValueOrDefault(); i++)
            {
                dummyClass.List_Max1 = dummyClass.List_Max1.Add("blah");
            }
            
            using(var validator = new BasicFormValidator<MaxCollectionElementsDummyClass>())
            {
                var validationErrors = await validator.ValidateAsync(dummyClass);
                if(expectError)
                {
                    ValidationErrorChecker.ErrorReported<MaxCollectionElementsDummyClass>(
                        validationErrors,
                        nameof(MaxCollectionElementsDummyClass.List_Max1),
                        new EnumerableMaxElements(maxLength).ErrorMessage
                    );
                }
                else
                {
                    ValidationErrorChecker.ErrorNotReported<MaxCollectionElementsDummyClass>(
                        validationErrors,
                        nameof(MaxCollectionElementsDummyClass.List_Max1),
                        new EnumerableMaxElements(maxLength).ErrorMessage
                    );
                }
            }
        }

        class MaxCollectionElementsDummyClass
        {
            [EnumerableMaxElements(1)]
            public ImmutableList<string> List_Max1 { get; set; }
        }
    }
}