namespace WebApplication1.Middlewares;

public class Authentication:IMiddleware
{
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
                var s=context.Response.Body.ToString();
                //Console.WriteLine(s);
                await next(context);
        }
}