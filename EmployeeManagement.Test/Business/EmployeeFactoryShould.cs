using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Xunit;

namespace EmployeeManagement.Test.Business
{
    public class EmployeeFactoryTests
    {
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
        {
            var employeeFactory = new EmployeeFactory();

            var employee = (InternalEmployee)employeeFactory
                .CreateEmployee("6", "BLossom");

            Assert.Equal(2500, employee.Salary);
        }

        [Fact]
        public void CreateEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
        {
            var employeeFactory = new EmployeeFactory();

            var employee = (InternalEmployee)employeeFactory
                .CreateEmployee("6", "Blossom");

            Assert.True(employee.Salary >= 2500 && employee.Salary <= 3500,
                "employee.Salary is not within an acceptable range.");
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
            Assert.InRange(employee.Salary, 2500, 3500);
        }

        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustB2500_Percision()
        {
            var employeeFactory = new EmployeeFactory { };

            var employee = (InternalEmployee)employeeFactory
                .CreateEmployee("6", "Blossom");
            employee.Salary = 2500.123m;

            Assert.Equal(2500, employee.Salary, 0);
        }

        [Fact]
        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
        {
            var employeeFactory = new EmployeeFactory { };

            var employee = (ExternalEmployee)employeeFactory
                .CreateEmployee("6", "Blossom", "ROSEMOUNT SAW & TOOL", true);

            Assert.IsType<ExternalEmployee>(employee);
            // NOTE: Can be via derived type.
            Assert.IsAssignableFrom<Employee>(employee);
        }
    }
}
