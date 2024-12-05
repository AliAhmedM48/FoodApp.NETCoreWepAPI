namespace Food.App.API.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var requestId = Guid.NewGuid();
            _logger.LogError(ex.Message, $"RequestId: {requestId} - An error occurred while processing the request.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";


            if (_env.IsDevelopment())
            {
                var errorResponse = new
                {
                    message = ex.Message,
                    stackTrace = ex.StackTrace,
                    requestId = requestId.ToString()
                };
                await context.Response.WriteAsJsonAsync(errorResponse);

            }
            else
            {
                var errorResponse = new
                {
                    message = "An unexpected error occurred. Please try again later.",
                    requestId = requestId.ToString()
                };
                await context.Response.WriteAsJsonAsync(errorResponse);

            }

        }
    }
}
