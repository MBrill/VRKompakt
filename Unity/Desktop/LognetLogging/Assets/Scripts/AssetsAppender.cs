using UnityEngine;
using log4net.Core;
using System.IO;
using System;
using JetBrains.Annotations;

/// <summary>
/// Log4Net-Appende, der Ausgaben in eine Datei
/// unterhalb von Assets schreibt..
/// </summary>
/// <remarks>
/// Als Default wird das Verzeichnis StreamingAssets eingesetzt.
/// Der Dateiname lautet LogOutput.txt.
///
/// Als Default werden die Ausgaben in die Datei nicht angehängt.
/// Ist das nicht gewünscht kann der Quelltext im Konstruktor
/// verändert werden.
/// 
/// In Android-Anwendungen befindet sich dieses Verzeichnis
/// laut Unity-Dokumentation im apk-file!
/// </remarks>
public class AssetsAppender : log4net.Appender.AppenderSkeleton
{
    /// <summary>
    /// Überschreiben der Append-Funktion
    /// </summary>
    /// <param name="loggingEvent">Daten des Events aus log4net</param>
    protected override void Append(LoggingEvent loggingEvent)
    {
        var message = RenderLoggingEvent(loggingEvent);
        Debug.Log("In Append von AssetsAppender");
        _writer.WriteLine(message);
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
    private static readonly string _filepath = Application.dataPath + "/Output.logs";
    private static readonly System.IO.FileStream _fileStream = new FileStream(_filepath, 
           FileMode.OpenOrCreate,
           FileAccess.ReadWrite);
    private static readonly System.IO.StreamWriter _writer = 
        new System.IO.StreamWriter(_fileStream);
}