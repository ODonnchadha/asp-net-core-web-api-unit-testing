namespace EmployeeManagement.Middleware
{
    public class EmployeeManagementSecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public EmployeeManagementSecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Adding securty headers to the response.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            IHeaderDictionary headers = context.Response.Headers;
             
            headers["Content-Security-Policy"] = "default-src 'self';frame-ancestors 'none';"; 
            headers["X-Content-Type-Options"] = "nosniff"; 

            await _next(context);
        }
    }
}

 