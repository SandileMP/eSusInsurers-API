using ILogger = Serilog.ILogger;

namespace eSusInsurers.Common
{
    public class LoggerContext<TRequest> : ILoggerContext<TRequest>
    {
        private readonly ILogger _logClient;

        public LoggerContext(ILogger logClient)
        {
            _logClient = logClient;
        }

        public Task LogMessageAsync(TRequest request, string? response, CancellationToken cancellationToken, Dictionary<string, string>? customProperties = null)
        {
            _logClient.LogInformation(response, customProperties);
            return Task.FromResult(true);
        }

        public Task LogErrorAsync(TRequest request, Exception ex, string errorMessage, CancellationToken cancellationToken, Dictionary<string, string>? customProperties = null)
        {
            _logClient.LogError(ex, customProperties);

            return Task.FromResult(true);
        }
    }
}
