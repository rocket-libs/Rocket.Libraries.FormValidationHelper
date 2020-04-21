using System;
using Xunit;
using Rocket.Libraries.FormValidationHelper.Attributes.InBuilt.DateAndTime;
namespace Rocket.Libraries.FormValidationHelperTests.Attributes.InBuilt.DateAndTime
{
    public class DateMustBeFutureTests
    {
        [Theory]
        [InlineData(-1,true)]
        [InlineData(1,false)]
        public void MustBeFutureDateWorks(int minutesToAdd, bool expectError)
        {
            var dateValue = DateTime.Now.AddMinutes(minutesToAdd);
            var validationResult = new DateMustBeFuture().ValidationFailed(dateValue);

            Assert.Equal(expectError, validationResult);
        }

        [Fact]
        public void MustBeFutureDateConveniencePropertyReturnsCorrectErrorMessage()
        {
            Assert.Equal(new DateMustBeFuture().ErrorMessage, DateMustBeFuture.MessageOnError);
        }
    }
}