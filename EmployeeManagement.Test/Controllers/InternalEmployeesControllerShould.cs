using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EmployeeManagement.Test.Controllers
{
    public class InternalEmployeesControllerShould
    {
        private readonly InternalEmployeesController _controller;
        private readonly InternalEmployee _employee;
        public InternalEmployeesControllerShould()
        {
            _employee = new InternalEmployee("Flann", "O'Brien", 2, 3000, false, 1);
            
            // NOTE: Beware instance of the return type for this mapper.
            // If we assert something that uses the mapper, we fail.
            // We don't want to asset on mapping code when we mock the mapping.

            //var mapper = new Mock<IMapper>();
            //mapper.Setup(m =>
            //    m.Map<InternalEmployee, Models.InternalEmployeeDto>
            //    (It.IsAny<InternalEmployee>())).Returns(new Models.InternalEmployeeDto { });

            // Use the profile configuration.
            var configuration = new MapperConfiguration(config =>
                config.AddProfile<MapperProfiles.EmployeeProfile>());
            var mapper = new Mapper(configuration);

            var service = new Mock<IEmployeeService>();
            service.Setup(s => s.FetchInternalEmployeesAsync()).ReturnsAsync(
                new List<InternalEmployee>
                {
                    _employee,
                    new InternalEmployee("James","Joyce", 3, 4000, true, 2),
                    new InternalEmployee("Patrick","McCabe", 4, 5000, false, 3),
                });

            //_controller = new InternalEmployeesController(service.Object, mapper.Object);
            _controller = new InternalEmployeesController(service.Object, mapper);
        }

        [Fact()]
        public async Task GetInternalEmployees_GetAction_ReturnsOkObjectWithCorrectAmountOfInternalEmployees()
        {
            // Act.
            var result = await _controller.GetInternalEmployees();

            // Assert.
            var actionResult =
                Assert.IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(
                    result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var models = 
                Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>(
                    objectResult.Value);
            Assert.Equal(3, models.Count());

            var employee = models.First();
            Assert.Equal(_employee.Id, employee.Id);
            Assert.Equal(_employee.FirstName, employee.FirstName);
            Assert.Equal(_employee.LastName, employee.LastName);
            Assert.Equal(_employee.Salary, employee.Salary);
            Assert.Equal(_employee.SuggestedBonus, employee.SuggestedBonus);
            Assert.Equal(_employee.YearsInService, employee.YearsInService);
        }

        [Fact()]
        public async Task GetInternalEmployees_GetAction_MustReturnNumberOfInputtedInternalEmployees()
        {
            // Act.
            var result = await _controller.GetInternalEmployees();

            // Assert.
            var actionResult =
                Assert.IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);
            Assert.Equal(3, ((IEnumerable<Models.InternalEmployeeDto>)
                ((OkObjectResult)actionResult.Result).Value).Count());
        }

        [Fact()]
        public async Task GetInternalEmployees_GetAction_MustReturnIEnumerableOfInternalEmployees()
        {
            // Act.
            var result = await _controller.GetInternalEmployees();

            // Assert.
            var actionResult =
                Assert.IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);
            Assert.IsAssignableFrom<IEnumerable<Models.InternalEmployeeDto>>(
                ((OkObjectResult)actionResult.Result).Value);
        }

        [Fact()]
        public async Task GetInternalEmployees_GetAction_MustReturnOkObjectResult()
        {
            // Act.
            var result = await _controller.GetInternalEmployees();

            // Assert.
            var actionResult = 
                Assert.IsType<ActionResult<IEnumerable<Models.InternalEmployeeDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }
    }
}
