using ILogger = Serilog.ILogger;

namespace eSusInsurers.Common.Logging
{
    public static class SeriLoggerAppInsights
    {
        public static void LogError(this ILogger seriLogLogger, Exception ex, Dictionary<string, string>? customProperties = null)
        {
            CustomSeriLogger(seriLogLogger, customProperties).Fatal(ex, ex.Message);
        }

        public static void LogInformation(this ILogger seriLogLogger, string? message, Dictionary<string, string>? customProperties = null)
        {
            CustomSeriLogger(seriLogLogger, customProperties).Information(message ?? string.Empty);
        }

        private static ILogger CustomSeriLogger(ILogger seriLogLogger, Dictionary<string, string>? customProperties)
        {
            var cLogger = seriLogLogger;

            return customProperties == null ? cLogger :
                customProperties.Aggregate(cLogger, (current, dd) => current.ForContext(dd.Key, dd.Value));
        }
    }
}
