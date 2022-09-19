using EmployeeManagement.Middleware;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace EmployeeManagement.Test.Middleware
{
    public class EmployeeManagementSecurityHeadersMiddlewareShould
    {
        [Fact()]
        public async Task InvokeAsync_Invoke_SetExpectedResponseHeaders()
        {
            // Arrange.
            var context = new DefaultHttpContext { };
            RequestDelegate next = (HttpContext context) => Task.CompletedTask;

            var middleware = new EmployeeManagementSecurityHeadersMiddleware(next);

            // Act.
            await middleware.InvokeAsync(context);

            // Assert.
            var h1 = context.Response.Headers["Content-Security-Policy"].ToString();
            var h2 = context.Response.Headers["X-Content-Type-Options"].ToString();

            Assert.Equal("default-src 'self';frame-ancestors 'none';", h1);
            Assert.Equal("nosniff", h2);
        }
    }
}
