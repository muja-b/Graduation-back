namespace WebApplication1.Middlewares;

public class Authentication:IMiddleware
{
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
                await context.Response.WriteAsync("Hello from Custom Middleware");
                await next(context);
        }
}