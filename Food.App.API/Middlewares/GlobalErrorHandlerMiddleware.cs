namespace Food.App.API.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

    public GlobalErrorHandlerMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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

            var errorResponse = new
            {
                message = ex.Message,//"An unexpected error occurred. Please try again later.",
                requestId = requestId.ToString()
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
