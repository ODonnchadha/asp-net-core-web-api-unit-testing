using Xunit;

namespace EmployeeManagement.Test.TestData
{
    public class EmployeeServiceStronglyTypedTestData : TheoryData<int, bool>
    {
        public EmployeeServiceStronglyTypedTestData()
        {
            Add(100, true);
            Add(200, false);
        }
    }
}
