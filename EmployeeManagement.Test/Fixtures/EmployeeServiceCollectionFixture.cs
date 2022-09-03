using Xunit;

namespace EmployeeManagement.Test.Fixtures
{
    /// <summary>
    /// Wrapper.
    /// </summary>
    [CollectionDefinition("EmployeeServiceCollection")]
    public class EmployeeServiceCollectionFixture : ICollectionFixture<EmployeeServiceFixture>
    {
    }
}
