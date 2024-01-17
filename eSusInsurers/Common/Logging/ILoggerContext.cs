namespace eSusInsurers.Common.Logging
{
    public interface ILoggerContext<TRequest>
    {
        public Task LogMessageAsync(TRequest request, string? response, CancellationToken cancellationToken, Dictionary<string, string>? customProperties = null);

        public Task LogErrorAsync(TRequest request, Exception ex, string errorMessage, CancellationToken cancellationToken, Dictionary<string, string>? customProperties = null);
    }
}
