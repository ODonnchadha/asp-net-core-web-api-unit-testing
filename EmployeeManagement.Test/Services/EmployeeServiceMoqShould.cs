using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Repositories;
using Moq;
using Xunit;

namespace EmployeeManagement.Test.Services
{
    public class EmployeeServiceMoqShould
    {
        [Fact()]
        public async Task FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_Async()
        {
            // Arrange.
            var repository = new Mock<IEmployeeManagementRepository>();
            repository.Setup(
                r => r.GetInternalEmployeeAsync(It.IsAny<Guid>())).ReturnsAsync(
                new InternalEmployee("Flann", "O'Brien", 2, 2500, false, 2)
                {
                    AttendedCourses = new List<Course>
                    {
                        new Course("AT SWIM-TWO-BIRDS"),
                        new Course("THE THIRD POLICEMAN")
                    }
                });

            var service = new EmployeeService(repository.Object, new Mock<EmployeeFactory>().Object);

            // Act.
            var employee = await service.FetchInternalEmployeeAsync(Guid.Empty);

            // Assert.
            Assert.Equal(400, employee?.SuggestedBonus);
        }

        [Fact()]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated()
        {
            // Arrange.
            var repository = new Mock<IEmployeeManagementRepository>();
            repository.Setup(
                r => r.GetInternalEmployee(It.IsAny<Guid>())).Returns(
                new InternalEmployee("Flann", "O'Brien", 2, 2500, false, 2)
                {
                    AttendedCourses = new List<Course>
                    {
                        new Course("AT SWIM-TWO-BIRDS"),
                        new Course("THE THIRD POLICEMAN")
                    }
                });

            var service = new EmployeeService(repository.Object, new Mock<EmployeeFactory>().Object);

            // Act.
            var employee = service.FetchInternalEmployee(Guid.Empty);

            // Assert.
            Assert.Equal(400, employee?.SuggestedBonus);
        }

        [Fact()]
        public void FetchInternalEmployee_InternalEmployeeCreated_SuggestedBonusMustBeCalculated_Moq()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };

            var factory = new Mock<EmployeeFactory>();
            // Moq is better-suited to Mock classes with overrideable behavior, abstract classes, and interfaces.
            // NOTE: The parameter matching. e.g.: "Flann" With two pontential matches, the second wins.
            factory.Setup(
                f => f.CreateEmployee(
                    It.Is<string>(s => s.Contains("Flann")), It.IsAny<string>(), null, false)).Returns(
                new InternalEmployee("Flann", "O'Brien", 5, 2500, false, 1));

            var service = new EmployeeService(repository, factory.Object);

            // Act.
            var employee = service.CreateInternalEmployee("Flann", "O'Brien");

            // Assert.
            Assert.Equal(1000, employee?.SuggestedBonus);
        }

        [Fact()]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_Moq()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var factory = new Mock<EmployeeFactory>();

            var service = new EmployeeService(repository, factory.Object);

            // Act.
            var employee = service.FetchInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            // Assert.
            Assert.Equal(400, employee?.SuggestedBonus);
        }

        [Fact()]
        public void FetchInternalEmployee_EmployeeFetched_SuggestedBonusMustBeCalculated_Concrete()
        {
            // Arrange.
            var repository = new EmployeeManagementTestDataRepository { };
            var factory = new EmployeeFactory { };

            var service = new EmployeeService(repository, factory);

            // Act.
            var employee = service.FetchInternalEmployee(Guid.Parse("72f2f5fe-e50c-4966-8420-d50258aefdcb"));

            // Assert.
            Assert.Equal(400, employee?.SuggestedBonus);
        }
    }
}
