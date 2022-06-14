using UnityEngine;
using log4net.Appender;
using log4net.Core;

/// <summary>
/// Log4Net-Appende, der Ausgaben in eine Datei
/// unterhalb von Assets schreibt..
/// </summary>
/// <remarks>
/// Als Default wird das Verzeichnis StreamingAssets eingesetzt.
/// Der Dateiname lautet LogOutput.txt.
///
/// Als Default werden die Ausgaben in die Datei angehängt.
/// Ist das nicht gewünscht kann der Quelltext im Konstruktor
/// verändert werden.
/// 
/// In Android-Anwendungen befindet sich dieses Verzeichnis
/// laut Unity-Dokumentation im apk-file!
/// </remarks>
public class AssetsAppender : AppenderSkeleton
{
    /// <summary>
    /// Überschreiben der Append-Funktion
    /// </summary>
    /// <param name="loggingEvent">Daten des Events aus log4net</param>
    protected override void Append(LoggingEvent loggingEvent)
    {
        file.WriteLine(RenderLoggingEvent(loggingEvent));
    }
    
    /// <summary>
    /// Instanz von StreamWriter mit Pfad und Dateinamen.
    /// </summary>
    /// <remarks>
    /// Den Pfad lesen wir aus der Variablen Application.StreamingAssetsPath.
    ///
    /// Aktuell hängen wir die Meldungen immer an. Das bedeutet, dass die
    /// Datei sehr groß werden kann.
    ///
    /// Wenn dies nicht gewünscht ist in dieser Funktion append auf false setzen.
    /// </remarks>
    private System.IO.StreamWriter file = 
        new System.IO.StreamWriter(
            $"{Application.streamingAssetsPath}/LogOutput.txt", 
            false
            );
}