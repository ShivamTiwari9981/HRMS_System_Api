using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _filePath;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "error.txt");
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        if (!Directory.Exists("Logs"))
            Directory.CreateDirectory("Logs");

        var errorLog = $"Time: {DateTime.Now}\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}\n\n";
        await File.AppendAllTextAsync(_filePath, errorLog);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error"
        });

        await context.Response.WriteAsync(result);
    }
}