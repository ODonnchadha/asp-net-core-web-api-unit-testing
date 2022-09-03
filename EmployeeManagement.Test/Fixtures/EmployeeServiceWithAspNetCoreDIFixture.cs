using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Test.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceWithAspNetCoreDIFixture : IDisposable
    {
        private readonly ServiceProvider _provider;
#pragma warning disable CS8603 // Possible null reference return.
        public IEmployeeManagementRepository EmployeeManagementRepository => _provider.GetService<IEmployeeManagementRepository>();
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning disable CS8603 // Possible null reference return.
        public IEmployeeService EmployeeService { get { return _provider.GetService<IEmployeeService>(); } }
#pragma warning restore CS8603 // Possible null reference return.
        public EmployeeServiceWithAspNetCoreDIFixture()
        {
            var services = new ServiceCollection { };

            services.AddScoped<EmployeeFactory>();
            services.AddScoped< IEmployeeManagementRepository, EmployeeManagementTestDataRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            _provider = services.BuildServiceProvider();
        }
        public void Dispose() { }
    }
}
