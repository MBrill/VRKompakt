using UnityEngine;
using log4net.Appender;
using log4net.Core;

/// <summary> 
/// Log4Net-Appender für die Unity Console.
/// </summary>
/// <remarks>
///  Je nach Level des Logging-Outputs werden verschiedene Funktionen
/// der Unity-Klasse Debug verwendet.
///
/// Quelle: https://stackoverflow.com/questions/23796412/how-to-use-use-log4net-with-unity/
/// </remarks>
public class UnityConsoleAppender : AppenderSkeleton
{
    /// <summary>
    /// Implementierung der Ausgabe
    /// </summary>
    /// <remarks>
    /// Wir stufen alles mit einem Log-Level größer oder gleich Error
    /// als Error ein.
    ///
    ///  Alles unterhalb von warning wird als Warning ausgegeben.
    ///
    /// Alles weitere wird einfach ausgegeben..
    /// </remarks>
    /// <param name="loggingEvent"
    /// Logging-Event mit den Inhalten,
    /// die wir ausgeben.
    //.</param>
    protected override void Append(LoggingEvent loggingEvent)
    {
        var message = RenderLoggingEvent(loggingEvent);
        
        if (Level.Compare(loggingEvent.Level, Level.Error) >= 0)
        {
            Debug.LogError(message);
        }
        else if (Level.Compare(loggingEvent.Level, Level.Warn) >= 0)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }
}