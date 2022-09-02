using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Test.Repositories;
using Xunit;

namespace EmployeeManagement.Test.Services
{
    public class EmployeeServiceShould
    {
        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPredicate()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var service = new EmployeeService(repository, new EmployeeFactory { });

            // Act.
            var employee = service.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Contains(employee.AttendedCourses, 
                c => c.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));
        }

        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var service = new EmployeeService(repository, new EmployeeFactory { });
            var courses = repository.GetCourses(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act.
            var employee = service.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Equal(courses, employee.AttendedCourses);
        }

        [Fact()]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses_Async()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var service = new EmployeeService(repository, new EmployeeFactory { });
            var courses = await repository.GetCoursesAsync(
                Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act.
            var employee = await service.CreateInternalEmployeeAsync("Flann", "O'Brien");

            // Assert. Not async.
            Assert.Equal(courses, employee.AttendedCourses);
        }

        [Fact()]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown_Async()
        {
            // Arrange.
            var service = new EmployeeService(
                new EmployeeManagementTestDataRepository { }, new EmployeeFactory { });
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await service.GiveRaiseAsync(employee, 50));
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
            var service = new EmployeeService(
                new EmployeeManagementTestDataRepository { }, new EmployeeFactory { });
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await service.GiveRaiseAsync(employee, 50));
        }

        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var service = new EmployeeService(repository, new EmployeeFactory { });

            // Act.
            var employee = service.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            foreach (var course in employee.AttendedCourses)
            {
                Assert.False(course.IsNew);
            }
            // Clearer out-of-box collection(s) messaging.
            Assert.All(employee.AttendedCourses, course => Assert.False(course.IsNew));
        }

        [Fact()]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var service = new EmployeeService(repository, new EmployeeFactory { });
            var course = repository.GetCourse(Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            // Act.
            var employee = service.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Contains(course, employee.AttendedCourses);
        }

        [Fact()]
        public void NotifyOfAbsence_EmployeeIsAbsent_OnEmployeeIsAbsentMustBeTriggered()
        {
            // Arrange.
            var service = new EmployeeService(
                new EmployeeManagementTestDataRepository { }, new EmployeeFactory { });
            var employee = new InternalEmployee("Flann", "O'Brien", 5, 3000, false, 1);

            // Act & Assert.
            // NOTE: (1) Attach, (2) detach an event handler.
            // (3) Execute the actual code that raises the event.
            Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => service.EmployeeIsAbsent += handler,
                handler => service.EmployeeIsAbsent -= handler,
                () => service.NotifyOfAbsence(employee));
        }
    }
}
