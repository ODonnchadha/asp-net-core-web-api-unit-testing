using EmployeeManagement.DataAccess.Entities;
using Xunit;

namespace EmployeeManagement.Test.DataAccess.Entities
{
    public class EmployeeShould
    {
        [Fact()]
        public void EmployeeFullNamePropertyGetter_InputFOrstNameAndLastName_FullNameIsConcatenation()
        {
            var employee = new InternalEmployee("FIRST_NAME", "LAST_NAME", 0, 40, false, 1);

            employee.FirstName = "X";
            employee.LastName = "Y";

            Assert.Equal("X Y", employee.FullName, ignoreCase:true);
            Assert.StartsWith("X", employee.FullName);
            Assert.EndsWith("Y", employee.FullName);
            Assert.Contains(" ", employee.FullName);
        }
    }
}
