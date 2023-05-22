using Microsoft.AspNetCore.Http;
using Serdiuk.ToDoList.Application.Exceptions;

namespace Serdiuk.ToDoList.Application.Middlewares
{
    public class TodoExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public TodoExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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
        }
    }
}
