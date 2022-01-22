using Microsoft.AspNetCore.Builder;

namespace MoviesApp.Middleware
{
    //чтобы работал RequestLogMiddleware, его надо зарегать
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLog( //по соглашению: Use + название мидлвеара
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}