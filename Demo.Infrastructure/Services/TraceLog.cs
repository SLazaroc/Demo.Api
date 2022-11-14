using Microsoft.Extensions.Logging;

namespace Demo.Infrastructure.Services
{
    public class TraceLog : ITraceLog
    {
        private readonly ILogger<TraceLog> logger;

        public TraceLog(
            ILogger<TraceLog> logger)
        {
            this.logger = logger;
        }
        public void logException(Exception ex, int? referenceTypeId, string referenceId)
        {
            this.logger.LogError(referenceId, referenceTypeId, referenceId, ex);    
        }

        public void LogInfo(int? referenceTypeId, string referenceId, string message)
        {
            this.logger.LogInformation(referenceId, referenceTypeId, message);
        }

        public void LogError(int? referenceTypeId, string referenceId, string message)
        {
            this.logger.LogError(referenceId, referenceTypeId, message);
        }
    }
}
