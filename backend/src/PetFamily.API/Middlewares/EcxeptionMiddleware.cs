using PetFamily.API.Response;

namespace PetFamily.API.Middlewares
{
    public class EcxeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<EcxeptionMiddleware> _logger;

        public EcxeptionMiddleware(RequestDelegate next, ILogger<EcxeptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                var responseError = new ResponseError("server.internal", ex.Message, null);
                var envelope = Envelope.Error([responseError]);
            

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(ex);
            }

        }
    }

    public static class EcxeptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EcxeptionMiddleware>();
        }
    }
}
