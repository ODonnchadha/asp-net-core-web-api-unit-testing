using EmployeeManagement.DataAccess.Entities;
using Xunit;

namespace EmployeeManagement.Test.DataAccess.Entities
{
    public class CourseShould
    {
        [Fact()]
        public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
        {
            var c = new Course("MANAGEMENT 101");

            Assert.True(c.IsNew);
        }
    }
}
