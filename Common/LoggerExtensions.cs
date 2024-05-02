using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common
{
    public static class LoggerExtensions
    {
        public static void LogErrorStack<T>(this ILogger<T> logger, Exception ex) =>
            logger?.LogError(ex, "{MethodName} I/O error '{Message}' at {DateTime}",
                        new StackTrace(ex).GetFrame(0)?.GetMethod()?.Name, ex.Message, DateTimeOffset.Now);

        public static void LogTraceStack<T>(this ILogger<T> logger, Exception ex) =>
            logger?.LogTrace(ex, "{MethodName} I/O error '{Message}' at {DateTime}",
                        new StackTrace(ex).GetFrame(0)?.GetMethod()?.Name, ex.Message, DateTimeOffset.Now);
    }
}
