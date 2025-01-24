using Microsoft.AspNetCore.Diagnostics;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.DealerAPI.Infrastructure.ExceptionHandling
{
    public class SelfWaiterDealerExceptionHandler(ILogger<SelfWaiterDealerExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not SelfWaiterException)
                return false;

            var stackTrace = new System.Diagnostics.StackTrace(exception, true);
            string declaringType = string.Empty;
            string methodName = string.Empty;
            if (stackTrace != null)
            {
                var frame = stackTrace.GetFrame(0);
                declaringType = frame?.GetMethod()?.DeclaringType?.FullName ?? string.Empty;
                methodName = frame?.GetMethod()?.Name ?? string.Empty;
            }

            _logger.LogError(exception, "An error occurred in {declaringType} . {methodName} with SelfWaiterDealerException . Error message : {exception.Message}", declaringType, methodName, exception.Message);

            string errorMessage = $"Bir hata oluştu. Hata mesajı :  {exception.Message}";
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Title = "Server Error - Sunucu Hatası",
                Status = httpContext.Response.StatusCode,
                Message = errorMessage,
                ToastMessage = exception.Message,
                IsShowable = true,
            });

            return true;

        }
    }
}
