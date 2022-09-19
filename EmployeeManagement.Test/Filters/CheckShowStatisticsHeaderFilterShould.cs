using EmployeeManagement.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Xunit;

namespace EmployeeManagement.Test.Filters
{
    public class CheckShowStatisticsHeaderFilterShould
    {
        [Fact()]
        public void OnActionExecuting_InvokeWithoutSHowStatisticsHeader_ReturnsBadRequest()
        {
            // Arrange.
            var filter = new CheckShowStatisticsHeader { };
            var context = new ActionContext(
                new DefaultHttpContext { }, new(), new(), new());

            var executingContext = new ActionExecutingContext(
                context,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: null);

            // Act.
            filter.OnActionExecuting(executingContext);

            // Assert.
            Assert.IsType<BadRequestResult>(executingContext.Result);
        }
    }
}
