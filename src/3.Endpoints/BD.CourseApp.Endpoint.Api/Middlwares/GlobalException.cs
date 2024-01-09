using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BD.CourseApp.Endpoint.Api.Middlwares
{
    public class GlobalException:IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _host;
        private readonly ILogger<GlobalException> logger;

        public GlobalException(ILogger<GlobalException> logger, IWebHostEnvironment host)
        {
            this.logger = logger;
            _host = host;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (_host.IsProduction())
            {
                //add log
                var result = new ObjectResult("An error occured");
                result.StatusCode = 500;
                context.Result = result;
            }

            //add log
            logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
