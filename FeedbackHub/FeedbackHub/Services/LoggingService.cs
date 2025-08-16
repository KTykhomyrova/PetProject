using System.IO;

namespace FeedbackHub.Services
{
    public interface ILoggingService
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex);
    }

    public class LoggingService(string logFilePath) : ILoggingService
    {
        readonly string _logFilePath = logFilePath;

        public void LogInfo(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [INFO] {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void LogWarning(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [WARNING] {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void LogError(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void LogError(string message, Exception ex)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}\n{ex}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }
    }
}
