using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next; //ссылка на следующий делегат
        
        public RequestLogMiddleware(RequestDelegate next) //след делегат в конвейере
        {
            _next = next;
        }

        
        public async Task Invoke(HttpContext httpContext, ILogger<RequestLogMiddleware> logger) //вызов(invoking)
        {
            //код ДО
            //лог: путь(реквест, запрос), метод(гет, пост, делейт и т.д - метод реквеста)

            if (httpContext.Request.Path == "/Actors")
            {
                logger.LogTrace($"Request: {httpContext.Request.Path}  Method: {httpContext.Request.Method}");
                //передаем следующему
                await _next(httpContext);
            }
            else
            {
                await _next(httpContext);
            }
            
            //.......код после
        }
    }
}