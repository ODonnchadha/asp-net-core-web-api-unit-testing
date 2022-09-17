using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Test.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System.Text;
using System.Text.Json;
using Xunit;

namespace EmployeeManagement.Test.Controllers
{
    public class PromotionsControllerShould
    {
        /// <summary>
        /// NOTE:
        /// We *should* have mocked the service and the client (via handler.)
        /// Educational purposes only.
        /// </summary>
        [Fact()]
        public async void CreatePromotion_RequestPromotionForEligibleEmployee_MustPromoteEmployee()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var jobLevel = 1;

            var service = new Mock<IEmployeeService>();
            service.Setup(s => s.FetchInternalEmployeeAsync(It.IsAny<Guid>())).ReturnsAsync(
                new InternalEmployee("Flann", "O'Brien", 3, 3400, true, jobLevel)
                {
                    Id = id,
                    SuggestedBonus = 500
                });

            var handler = new Mock<HttpMessageHandler>();

            // NOTE: Protected().
            handler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpResponseMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(
                new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        JsonSerializer.Serialize(
                            new PromotionEligibility {  EligibleForPromotion = true },
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            }), Encoding.ASCII, "application/json")
                });

            var client = new HttpClient(handler.Object);
            var promotion = new PromotionService(client, new EmployeeManagementTestDataRepository { });

            var controller = new PromotionsController(service.Object, promotion);

            // Act.
            var result = await controller.CreatePromotion(
                new PromotionForCreationDto
                {
                    EmployeeId = id
                });

            // Assert.
            var objResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<PromotionResultDto>(objResult.Value);

            Assert.Equal(id, model.EmployeeId);
            Assert.Equal(++jobLevel, model.JobLevel);
        }
    }
}
