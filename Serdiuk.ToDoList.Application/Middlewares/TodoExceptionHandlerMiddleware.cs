using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serdiuk.ToDoList.Application.Exceptions;

namespace Serdiuk.ToDoList.Application.Middlewares
{
    public class TodoExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TodoExceptionHandlerMiddleware> logger;

        public TodoExceptionHandlerMiddleware(RequestDelegate next, ILogger<TodoExceptionHandlerMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(TodoNotFoundException e)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync(e.Message);
            }
            catch(TodoNotEnoughPermissionsException e)
            {
                httpContext.Response.StatusCode = 403;
                await httpContext.Response.WriteAsync(e.Message);
            }
            catch(TodoUnathorizeException e)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(e.Message);
            }
            if(httpContext.Response.StatusCode == 405)
            {
                logger.LogError("{Path}",httpContext.Request.Path);
                logger.LogError("{Method}", httpContext.Request.Method);
            }
        }
    }
}
