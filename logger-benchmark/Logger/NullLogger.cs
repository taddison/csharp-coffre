using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class NullLogger : ILogger
{
  private Dictionary<LogLevel,bool> _logLevelSettings;
  public NullLogger()
  {
    _logLevelSettings = new Dictionary<LogLevel, bool>() {
      [LogLevel.Error] = true,
      [LogLevel.Verbose] = false
    };
  }
  public void Log(LogLevel level, string logMessage)
  {
    if(_logLevelSettings[level])
    {
      NullLog(logMessage);
    }
  }

  public void Log(LogLevel level, string formatMessage, params object[] formatArguments)
  {
    if(_logLevelSettings[level])
    {
      NullLog(string.Format(formatMessage, formatArguments));
    }
  }

  [MethodImpl(MethodImplOptions.NoInlining)]
  private void NullLog(string logMessage)
  {
    return;
  }
}