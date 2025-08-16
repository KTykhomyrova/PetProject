using System.IO;

namespace FeedbackHub.Services
{
    public class LoggingService
    {
        private readonly string _logFilePath;

        public LoggingService()
        {
            _logFilePath = "app.log";
        }

        public void Log(string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [INFO] {message}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }

        public void LogError(string message, Exception ex)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {message}\n{ex}";
            File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
        }
    }
}
