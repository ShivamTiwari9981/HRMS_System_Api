using HRMS.Shared.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _filePath;
    private readonly LoggingSettings _settings;

    public RequestResponseLoggingMiddleware(RequestDelegate next, IOptions<LoggingSettings> options)
    {
        _next = next;
        _settings = options.Value;
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log.txt");
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            
            if (!_settings.IsWriteLog)
            {
                await _next(context);
                return;
            }

            // Create folder if not exists
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            // Request logging
            var request = context.Request;
            var requestBody = await ReadRequestBody(request);

            var log = new StringBuilder();
            log.AppendLine("===== REQUEST =====");
            log.AppendLine($"Time: {DateTime.Now}");
            log.AppendLine($"Method: {request.Method}");
            log.AppendLine($"Path: {request.Path}");
            log.AppendLine($"Body: {requestBody}");

            // Capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            var responseText = await ReadResponseBody(context.Response);

            log.AppendLine("===== RESPONSE =====");
            log.AppendLine($"Status Code: {context.Response.StatusCode}");
            log.AppendLine($"Response: {responseText}");
            log.AppendLine("=====================================\n");

            await File.AppendAllTextAsync(_filePath, log.ToString());

            // Copy response back
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            await File.AppendAllTextAsync(_filePath, $"Logging Error: {ex.Message}\n");
            throw;
        }
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }

    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return text;
    }
}