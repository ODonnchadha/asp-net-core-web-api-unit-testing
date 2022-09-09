using Xunit;

namespace EmployeeManagement.Test.TestData
{
    public class EmployeeServiceStronglyTypedTestDataFromFile : TheoryData<int, bool>
    {
        public EmployeeServiceStronglyTypedTestDataFromFile()
        {
            File.ReadAllLines("TestData/EmployeeServiceTestData.csv").ToList().ForEach(l =>
            {
                var split = l.Split(',');

                if (int.TryParse(split[0], out int raise) 
                    && bool.TryParse(split[1], out bool expectation))
                {
                    Add(raise, expectation);
                }
            });
        }
    }
}
