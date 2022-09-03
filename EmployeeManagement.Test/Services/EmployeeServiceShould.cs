using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Fixtures;
using Xunit;

namespace EmployeeManagement.Test.Services
{
    /// <summary>
    /// Test context is reused throughout and only needs to be initialized once. e.g.: Thread.Sleep(3000);
    /// </summary>
    public class EmployeeServiceShould : IClassFixture<EmployeeServiceFixture>
    {
        private readonly EmployeeServiceFixture _fixture;
        public EmployeeServiceShould(EmployeeServiceFixture fixture) => _fixture = fixture;

        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPredicate()
        {
            // Act.
            var employee = _fixture.EmployeeService.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Contains(employee.AttendedCourses, 
                c => c.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }

        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
        {
            // Arrange.
            var courses = _fixture.EmployeeManagementTestDataRepository.GetCourses(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act.
            var employee = _fixture.EmployeeService.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Equal(courses, employee.AttendedCourses);
        }

        [Fact()]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses_Async()
        {
            // Arrange.
            var courses = await _fixture.EmployeeManagementTestDataRepository.GetCoursesAsync(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act.
            var employee = await _fixture.EmployeeService.CreateInternalEmployeeAsync("Flann", "O'Brien");

            // Assert. Not async.
            Assert.Equal(courses, employee.AttendedCourses);
        }

        [Fact()]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown_Async()
        {
            // Arrange.
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await _fixture.EmployeeService.GiveRaiseAsync(employee, 50));
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
                async () => await _fixture.EmployeeService.GiveRaiseAsync(employee, 50));
        }

        /// <summary>
        /// NOTE: Assert.All(): Clearer out-of-box collection(s) messaging.
        /// </summary>
        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            // Act.
            var employee = _fixture.EmployeeService.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            foreach (var course in employee.AttendedCourses)
            {
                Assert.False(course.IsNew);
            }
            Assert.All(employee.AttendedCourses, course => Assert.False(course.IsNew));
        }

        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
        {
            // Arrange.
            var course = _fixture.EmployeeManagementTestDataRepository.GetCourse(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            // Act.
            var employee = _fixture.EmployeeService.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Contains(course, employee.AttendedCourses);
        }

        /// <summary>
        /// NOTE: (1) Attach, (2) detach an event handler.
        /// (3) Execute the actual code that raises the event.
        /// </summary>
        [Fact()]
        public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
        {
            // Arrange.
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => _fixture.EmployeeService.EmployeeIsAbsent += handler,
                handler => _fixture.EmployeeService.EmployeeIsAbsent -= handler,
                () => _fixture.EmployeeService.NotifyOfAbsence(employee));
        }
    }
}
