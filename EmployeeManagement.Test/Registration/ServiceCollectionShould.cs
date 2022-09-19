using EmployeeManagement.DataAccess.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EmployeeManagement.Test.Registration
{
    public class ServiceCollectionShould
    {
        [Fact()]
        public void RegisterDataServices_Execute_DataServicesAreRegistered()
        {
            // Arrange.
            var collection = new ServiceCollection { };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string> {
                        { "ConnectionStrings:EmploymentManagementDB", "X" }
                    }).Build();

            // Act.
            collection.RegisterDataServices(configuration);
            var provider = collection.BuildServiceProvider();

            // Assert.
            Assert.NotNull(
                provider.GetService<IEmployeeManagementRepository>());
            Assert.IsType<EmployeeManagementRepository>(
                provider.GetService<IEmployeeManagementRepository>());
        }
    }
}
