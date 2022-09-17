using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeeManagement.Test.Controllers
{
    public class StatisticsControllerShould
    {
        [Fact()]
        public void GetStatistics_InputFromHttpConnectionFeature_MustReturnInputtedIps()
        {
            // Arrange.
            var localIp = System.Net.IPAddress.Parse("111.111.111.111");
            var localPort = 5000;
            var remoteIp = System.Net.IPAddress.Parse("222.222.222.222");
            var remotePort = 8080;

            var features = new Mock<IFeatureCollection>();
            features.Setup(f => f.Get<IHttpConnectionFeature>()).Returns(
                new HttpConnectionFeature
                {
                    LocalIpAddress = localIp,
                    LocalPort = localPort,
                    RemoteIpAddress = remoteIp,
                    RemotePort = remotePort
                });

            var context = new Mock<HttpContext>();
            context.Setup(h => h.Features).Returns(features.Object);

            var configuration = new MapperConfiguration(
                m => m.AddProfile<MapperProfiles.StatisticsProfile>());
            var mapper = new Mapper(configuration);

            var controller = new StatisticsController(mapper);
            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext 
            { 
                HttpContext = context.Object
            };

            // Act.
            var result = controller.GetStatistics();

            // Assert.
            var actionResult = Assert.IsType<ActionResult<StatisticsDto>>(result);
            var objResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsType<StatisticsDto>(objResult.Value);

            Assert.Equal(localIp.ToString(), model.LocalIpAddress);
            Assert.Equal(localPort, model.LocalPort);
            Assert.Equal(remoteIp.ToString(), model.RemoteIpAddress);
            Assert.Equal(remotePort, model.RemotePort);
        }
    }
}
