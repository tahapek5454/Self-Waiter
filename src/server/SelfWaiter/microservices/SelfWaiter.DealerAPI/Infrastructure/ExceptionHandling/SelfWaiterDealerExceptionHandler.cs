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


            _logger.LogError(exception, "An error occurred SelfWaiterDealerException. Error message : {exception.Message}", exception.Message);

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
