using Microsoft.AspNetCore.Diagnostics;
using SelfWaiter.Shared.Core.Application.Utilities;

namespace SelfWaiter.FileAPI.Infrastructure.ExceptionHandling
{
    public class SelfWaiterFileValidationExceptionHandler(ILogger<SelfWaiterFileValidationExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not SelfWaiterValidationException)
                return false;

            _logger.LogError(exception, "An error occurred SelfWaiterDealerValidationException. Error message : {exception.Message}", exception.Message);

            string errorMessage = $"Bir hata oluştu. Hata mesajı :  {exception.Message}";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
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
