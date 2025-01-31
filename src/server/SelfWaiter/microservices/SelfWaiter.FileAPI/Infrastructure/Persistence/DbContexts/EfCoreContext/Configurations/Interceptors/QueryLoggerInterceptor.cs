using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SelfWaiter.FileAPI.Infrastructure.Persistence.DbContexts.EfCoreContext.Configurations.Interceptors
{
    public class QueryLoggerInterceptor(ILogger<AppDbContext> _logger) : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
        {
            var stopwatch = Stopwatch.StartNew();
            var resultData = base.ReaderExecuting(command, eventData, result);
            stopwatch.Stop();

            var elapsed = stopwatch.Elapsed;
            _logger.LogInformation($"Time: {elapsed.TotalMilliseconds} ms - {elapsed.TotalSeconds} s - {elapsed.TotalMinutes} min");
            return resultData;
        }
    }
}
