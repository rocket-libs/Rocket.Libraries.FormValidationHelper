using System;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.DateAndTime
{
    public class DateIsNotDefaultDateTimeValueTests
    {
        [Theory]
        [InlineData(0,true)]
        [InlineData(1,false)]
        public void MustBeFutureDateWorks(int millisecondsToAdd, bool expectError)
        {
            var dateValue = default(DateTime).AddMilliseconds(millisecondsToAdd);
            var validationResult = new DateIsNotDefaultDateTimeValue("").ValidationFailed(dateValue);

            Assert.Equal(expectError, validationResult);
        }
    }
}