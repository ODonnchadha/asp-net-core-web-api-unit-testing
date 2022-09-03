using EmployeeManagement.Test.Fixtures;
using Xunit;

namespace EmployeeManagement.Test.Services
{
    public class EmployeeServiceWithAspNetCoreDIShould : IClassFixture<EmployeeServiceWithAspNetCoreDIFixture>
    {
        private readonly EmployeeServiceWithAspNetCoreDIFixture _di;
        public EmployeeServiceWithAspNetCoreDIShould(EmployeeServiceWithAspNetCoreDIFixture di) => _di = di;

        [Fact()]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses_Async()
        {
            // Arrange.
            var courses = await _di.EmployeeManagementRepository.GetCoursesAsync(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act.
            var employee = await _di.EmployeeService.CreateInternalEmployeeAsync("Flann", "O'Brien");

            // Assert. Not async.
            Assert.Equal(courses, employee.AttendedCourses);
        }
    }
}
