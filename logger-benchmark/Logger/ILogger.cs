public interface ILogger
{
  void Log(LogLevel level, string logMessage);

  void Log(LogLevel level, string formatMessage, params object[] formatArguments);
}