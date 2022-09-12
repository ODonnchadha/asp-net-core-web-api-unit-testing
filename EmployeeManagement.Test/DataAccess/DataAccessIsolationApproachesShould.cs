using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.DbContexts;
using EmployeeManagement.DataAccess.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Sdk;

namespace EmployeeManagement.Test.DataAccess
{
    public class DataAccessIsolationApproachesShould
    {
        [Fact()]
        public async Task AttendCourseAsync_CourseAttended_SuggestedBonusMustCorrectlyBeRecalculated()
        {
            // Arrange.
            int bonus;

            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<EmployeeDbContext>().UseSqlite(connection);

            var context = new EmployeeDbContext(builder.Options);

            // Ensure that the in-memory database is created and that all migrations are executed.
            context.Database.Migrate();

            var repository = new EmployeeManagementRepository(context);
            var service = new EmployeeService(repository, new EmployeeFactory { });

            // Obtain course from the database. And an existing employee.
            var course = await repository.GetCourseAsync(Guid.Parse("844e14ce-c055-49e9-9610-855669c9859b"));
            var employee = await repository.GetInternalEmployeeAsync(
                Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            if (course == null || employee == null) 
            {
                throw new XunitException("Arrange failed.");
            }

            bonus = employee.YearsInService * (employee.AttendedCourses.Count + 1) * 100;

            // Act.
            await service.AttendCourseAsync(employee, course);

            // Assert.
            Assert.Equal(bonus, employee.SuggestedBonus);
        }
    }
}
