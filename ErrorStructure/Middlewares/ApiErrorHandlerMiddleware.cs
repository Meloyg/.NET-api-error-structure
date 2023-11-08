using ErrorStructure.Exceptions;
using ErrorStructure.Helpers;

namespace ErrorStructure.Middlewares;

public class ApiErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiErrorHandlerMiddleware> _logger;

    public ApiErrorHandlerMiddleware(RequestDelegate next, ILogger<ApiErrorHandlerMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ApiResponse<object>
        {
            Success = false,
        };

        switch (exception)
        {
            case UnauthorizedAccessException:
                response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Message = "Unauthorized access.";
                break;
            case NotFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                errorResponse.Message = exception.Message;
                break;
            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Message = exception.Message;
                break;
        }

        _logger.LogError(exception.Message);

        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}