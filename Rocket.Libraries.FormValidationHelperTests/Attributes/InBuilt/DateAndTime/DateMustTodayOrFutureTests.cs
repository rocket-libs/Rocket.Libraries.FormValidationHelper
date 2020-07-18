using System;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime;
using Xunit;

namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.DateAndTime
{
    public class DateMustTodayOrFutureTests
    {
        [Theory]
        [InlineData (-1, true)]
        [InlineData (0, false)]
        [InlineData (1, false)]
        public void DateMustTodayOrFutureWorks (int daysToAdd, bool expectError)
        {
            var dateValue = DateTime.Now.AddDays (daysToAdd);
            var startOfDateValue = new DateTime (dateValue.Year, dateValue.Month, dateValue.Day);
            var validationResult = new DateMustTodayOrFuture ().ValidationFailed (startOfDateValue);
            Assert.Equal (expectError, validationResult);
        }
    }
}