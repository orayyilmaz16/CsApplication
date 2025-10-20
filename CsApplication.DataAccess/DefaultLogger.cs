using CsApplication.Domain;
using Microsoft.Extensions.Logging;

namespace CsApplication.DataAccess
{
    public class DefaultLogger<T> : ILoggerService
    {
        private readonly ILogger<T> _logger;

        public DefaultLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Info(string message) => _logger.LogInformation(message);
        public void Warn(string message) => _logger.LogWarning(message);
        public void Error(string message, Exception ex = null) =>
            _logger.LogError(ex, message);

    }
}
