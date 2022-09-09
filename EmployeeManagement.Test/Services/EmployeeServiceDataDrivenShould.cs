using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using Xunit;

namespace EmployeeManagement.Test.Services
{
    [Collection("EmployeeServiceCollection")]
    public class EmployeeServiceDataDrivenShould
    {
        private readonly EmployeeServiceFixture _fixture;
        public EmployeeServiceDataDrivenShould(EmployeeServiceFixture fixture) => _fixture = fixture;

        /// <summary>
        /// This is good for "simple" use cases.
        /// </summary>
        /// <param name="id"></param>
        [Theory()]
        [InlineData("37e03ca7-c730-4351-834c-b66f280cdb01")]
        [InlineData("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e")]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse(
            Guid id)
        {
            // Act.
            var employee = _fixture.EmployeeService.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Contains(employee.AttendedCourses, c => c.Id == id);
        }

        [Fact()]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown_Async()
        {
            // Arrange.
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await _fixture.EmployeeService.GiveRaiseAsync(employee, 40));
        }

        /// <summary>
        /// NOTE: Here's a common mistake. This is not a good idea.
        /// When your assert is async, you need to await it. e.g.: await Assert.ThrowsAsync<T>
        /// Otherwise, your resulting Task is not returned to the xUnit framework.
        /// Thus, we cannot act upon it and have written a poor test.
        /// </summary>
        [Fact()]
        public void GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
        {
            // Arrange.
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await _fixture.EmployeeService.GiveRaiseAsync(employee, 007));
        }
    }
}
