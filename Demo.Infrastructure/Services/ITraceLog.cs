namespace Demo.Infrastructure.Services
{
    public interface ITraceLog
    {
        void LogInfo(int? referenceTypeId, string referenceId, string message);
        void LogError(int? referenceTypeId , string referenceId, string message);
        void logException(Exception ex, int? referenceTypeId , string referenceId);
    }
}
