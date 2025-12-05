/*
================================================================================
MANSERV Loan Account Management System - Local Testing API
Request Logging Middleware
================================================================================
Purpose: Log all HTTP requests and responses for debugging
Author: System Generated
Date: December 6, 2025
================================================================================
*/

using System.Diagnostics;
using System.Text;

namespace ManservLoanSystem.LocalTesting.API.Middleware
{
    /// <summary>
    /// Middleware to log all HTTP requests and responses
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = Guid.NewGuid().ToString("N").Substring(0, 8);

            // Log request
            await LogRequest(context, requestId);

            // Capture response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                // Call next middleware
                await _next(context);

                stopwatch.Stop();

                // Log response
                await LogResponse(context, requestId, stopwatch.ElapsedMilliseconds);

                // Copy response back to original stream
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "[{RequestId}] Exception occurred: {Message}", requestId, ex.Message);
                throw;
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task LogRequest(HttpContext context, string requestId)
        {
            var request = context.Request;

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"[{requestId}] ========== REQUEST ==========");
            logMessage.AppendLine($"Method: {request.Method}");
            logMessage.AppendLine($"Path: {request.Path}{request.QueryString}");
            logMessage.AppendLine($"Headers:");
            foreach (var header in request.Headers)
            {
                logMessage.AppendLine($"  {header.Key}: {header.Value}");
            }

            // Log request body for POST/PUT
            if (request.Method == "POST" || request.Method == "PUT")
            {
                request.EnableBuffering();
                var body = await new StreamReader(request.Body).ReadToEndAsync();
                request.Body.Position = 0;

                if (!string.IsNullOrWhiteSpace(body))
                {
                    logMessage.AppendLine($"Body: {body}");
                }
            }

            _logger.LogInformation(logMessage.ToString());
            Console.WriteLine(logMessage.ToString());
        }

        private async Task LogResponse(HttpContext context, string requestId, long elapsedMs)
        {
            var response = context.Response;

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"[{requestId}] ========== RESPONSE ==========");
            logMessage.AppendLine($"Status: {response.StatusCode}");
            logMessage.AppendLine($"Elapsed: {elapsedMs}ms");
            logMessage.AppendLine($"Headers:");
            foreach (var header in response.Headers)
            {
                logMessage.AppendLine($"  {header.Key}: {header.Value}");
            }

            // Log response body
            response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            if (!string.IsNullOrWhiteSpace(body))
            {
                // Truncate long responses
                var truncatedBody = body.Length > 1000 ? body.Substring(0, 1000) + "... (truncated)" : body;
                logMessage.AppendLine($"Body: {truncatedBody}");
            }

            _logger.LogInformation(logMessage.ToString());
            Console.WriteLine(logMessage.ToString());
        }
    }
}
