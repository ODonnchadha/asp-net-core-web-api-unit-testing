using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Repositories;
using Xunit;

namespace EmployeeManagement.Test.Services
{
    public class PromotionServiceShould
    {
        [Fact()]
        public async Task PromoteINternalEmployeeAsync_IsEligible_JobLevelMustBeIncreased()
        {
            // Arrange.
            int jobLevel = 1;

            var client = new HttpClient(new Test.HttpMessageHandlers.PromotionEligibilityHandler(true));
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, jobLevel);

            var service = new PromotionService(client, new EmployeeManagementTestDataRepository { });

            // Act.
            await service.PromoteInternalEmployeeAsync(employee);

            // Assert.
            Assert.Equal(++jobLevel, employee.JobLevel);
        }
    }
}
