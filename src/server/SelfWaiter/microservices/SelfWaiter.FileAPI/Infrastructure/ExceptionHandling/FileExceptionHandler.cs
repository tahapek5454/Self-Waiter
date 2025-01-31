using Microsoft.AspNetCore.Diagnostics;

namespace SelfWaiter.FileAPI.Infrastructure.ExceptionHandling
{
    public class FileExceptionHandler(ILogger<FileExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var stackTrace = new System.Diagnostics.StackTrace(exception, true);
            string declaringType = string.Empty;
            string methodName = string.Empty;
            if (stackTrace != null)
            {
                var frame = stackTrace.GetFrame(0);
                declaringType = frame?.GetMethod()?.DeclaringType?.FullName ?? string.Empty;
                methodName = frame?.GetMethod()?.Name ?? string.Empty;
            }

            _logger.LogError(exception, "An error occurred in {declaringType} . {methodName} with DealerException. Error message : {exception.Message}", declaringType, methodName, exception.Message);

            string errorMessage = $"Bir hata oluştu. Hata mesajı :  {exception.Message}";
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Title = "Server Error - Sunucu Hatası",
                Status = httpContext.Response.StatusCode,
                Message = errorMessage,
                ToastMessage = string.Empty,
                IsShowable = false,
            });

            return true;
        }
    }
}
