using UnityEngine;

/// <summary> 
/// Log4Net-Appender f�r die Unity Console.
/// </summary>
/// <remarks>
///  Alle Log4Net-Level werden mit Debug.Log ausgegeben,
/// ohne Ver�nderung.
///
/// Granularer kann man dies mit der Klasse
/// <code>UnityConsoleAppender</code>
/// durchf�hren.
/// </remarks>
public class UnityDebugAppender : log4net.Appender.AppenderSkeleton
{
    /// <summary>
    /// �berschreiben der Funktion Append
    /// </summary>
    /// <param name="loggingEvent">Logging-Event</param>
  protected override void Append(log4net.Core.LoggingEvent loggingEvent)
  {
    var message = RenderLoggingEvent(loggingEvent);
    Debug.Log(message);
  }
}
