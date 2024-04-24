using UnityEngine;

/// <summary> 
/// Log4Net-Appender für die Unity Console.
/// </summary>
/// <remarks>
///  Alle Log4Net-Level werden mit Debug.Log ausgegeben,
/// ohne Veränderung.
///
/// Granularer kann man dies mit der Klasse
/// <code>UnityConsoleAppender</code>
/// durchführen.
/// </remarks>
public class UnityDebugAppender : log4net.Appender.AppenderSkeleton
{
    /// <summary>
    /// Überschreiben der Funktion Append
    /// </summary>
    /// <param name="loggingEvent">Logging-Event</param>
  protected override void Append(log4net.Core.LoggingEvent loggingEvent)
  {
    var message = RenderLoggingEvent(loggingEvent);
    Debug.Log(message);
  }
}
