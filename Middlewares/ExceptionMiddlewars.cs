using System.Net;
using webApi_build_Real.Error;

namespace webApi_build_Real.Middlewares
{
    public class ExceptionMiddlewars
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewars> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddlewars(RequestDelegate next, ILogger<ExceptionMiddlewars> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ApiError response;
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                String message;
                var exceptionType = ex.GetType();
                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = HttpStatusCode.Forbidden;
                    message = "you are not authoried";
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "some Unkonow error Occoured";

                }
                if (_env.IsDevelopment())
                {
                    response = new ApiError((int)statusCode, ex.Message, ex.StackTrace.ToString());
                }
                else
                {
                    response = new ApiError((int)statusCode, message);
                }

                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }

    }
 }

    

