using Microsoft.AspNetCore.Diagnostics;

namespace SelfWaiter.DealerAPI.Infrastructure.ExceptionHandling
{
    public class DealerExceptionHandler(ILogger<DealerExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An error occurred DealerException. Error message : {exception.Message}", exception.Message);

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
