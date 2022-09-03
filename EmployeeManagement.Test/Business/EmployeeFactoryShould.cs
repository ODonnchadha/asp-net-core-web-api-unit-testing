using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Xunit;

namespace EmployeeManagement.Test.Business
{
    public class EmployeeFactoryShould : IDisposable
    {
        private EmployeeFactory _factory;

        /// <summary>
        /// NOTE: EmployeeFactory is "newed" up for each and every test.
        /// </summary>
        public EmployeeFactoryShould() => _factory = new EmployeeFactory { };
        public void Dispose() { }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            var employee = (InternalEmployee)_factory.CreateEmployee("6", "Blossom");

            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
        public void CreateEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
        {
            var employee = (InternalEmployee)_factory.CreateEmployee("6", "Blossom");

            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500,
                "employee.Salary is not within an acceptable range.");
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
            Assert.InRange(employee.Salary, 2500, 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustB2500_Percision()
        {
            var employee = (InternalEmployee)_factory.CreateEmployee("6", "Blossom");
            employee.Salary = 2500.123m;

            Assert.Equal(2500, employee.Salary, 0);
        }

        /// <summary>
        /// NOTE: Can be tested via derived type with IsAssignableFrom<T>().
        /// </summary>
        [Fact]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            var employee = (ExternalEmployee)_factory
                .CreateEmployee("6", "Blossom", "ROSEMOUNT SAW & TOOL", true);

            Assert.IsType<ExternalEmployee>(employee);
            Assert.IsAssignableFrom<Employee>(employee);
        }
    }
}
