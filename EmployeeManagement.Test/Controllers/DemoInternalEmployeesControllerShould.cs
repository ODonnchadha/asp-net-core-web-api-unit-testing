using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace EmployeeManagement.Test.Controllers
{
    public class DemoInternalEmployeesControllerShould
    {
        [Fact()]
        public async Task CreateInternalEmployees_InvalidInput_MustReturnBadRequest()
        {
            // Arrange.
            var service = new Mock<IEmployeeService>();
            var mapper = new Mock<IMapper>();

            var controller = new DemoInternalEmployeesController(
                service.Object, mapper.Object);

            controller.ModelState.AddModelError("FirstName", "Required");

            // Act.
           var result = await controller.CreateInternalEmployee(new InternalEmployeeForCreationDto { });

            // Assert:
            var actionResult = Assert.IsType<ActionResult<Models.InternalEmployeeDto>>(result);
            var request = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact()]
        public void GetProtectedInternalEmployees_GetActionForUserInAdminRole_Mocked_MustRedirectToGetInternalEmployeesOnProtected()
        {
            // Arrange.
            var service = new Mock<IEmployeeService>();
            var mapper = new Mock<IMapper>();

            var controller = new DemoInternalEmployeesController(
                service.Object, mapper.Object);

            var principle = new Mock<ClaimsPrincipal>();
            principle.Setup(
                p => p.IsInRole(It.Is<string>(
                    str => str == "Admin"))).Returns(true);

            var context = new Mock<HttpContext>();
            context.Setup(c => c.User).Returns(principle.Object);

            controller.ControllerContext = 
                new ControllerContext { HttpContext = context.Object };

            // Act.
            var result = controller.GetProtectedInternalEmployees();

            // Assert:
            Assert.IsAssignableFrom<IActionResult>(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("GetInternalEmployees", redirectToActionResult.ActionName);
            Assert.Equal("ProtectedInternalEmployees", redirectToActionResult.ControllerName);
        }

        [Fact()]
        public void GetProtectedInternalEmployees_GetActionForUserInAdminRole_Concrete_MustRedirectToGetInternalEmployeesOnProtected()
        {
            // Arrange.
            var service = new Mock<IEmployeeService>();
            var mapper = new Mock<IMapper>();

            var controller = new DemoInternalEmployeesController(
                service.Object, mapper.Object);

            var user = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Flann"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var context = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(
                    new ClaimsIdentity(user, "AUTHENTICATION_TYPE"))
            };

            controller.ControllerContext = new ControllerContext { HttpContext = context };

            // Act.
            var result = controller.GetProtectedInternalEmployees();

            // Assert:
            Assert.IsAssignableFrom<IActionResult>(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("GetInternalEmployees", redirectToActionResult.ActionName);
            Assert.Equal("ProtectedInternalEmployees", redirectToActionResult.ControllerName);
        }
    }
}
