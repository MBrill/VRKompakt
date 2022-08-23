using System.IO;
using UnityEngine;
using log4net.Core;


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
public class StreamingAssetsAppender : log4net.Appender.AppenderSkeleton
{
    /// <summary>
    /// Default-Konstruktor
    /// </summary>
    /// <remarks>
    /// Als Default-Dateiname verwenden wir
    /// <code>loggingExample.log</code>
    /// im StreamingAssets-Verzeichnis des Unity-Projekts.
    /// </remarks>
    public StreamingAssetsAppender()
    {
         string file = "loggingExample.log";


        var filePath = Application.streamingAssetsPath  + "/" + file;
        m_FileStream = new FileStream(filePath,
            FileMode.OpenOrCreate, 
            FileAccess.ReadWrite);

        try
        {
            m_StreamWriter = new StreamWriter(m_FileStream);
            m_StreamWriter.AutoFlush = true;
        }
        catch (FileNotFoundException ioEx)
        {
            Debug.LogError(ioEx.Message);
        }
    }

    /// <summary>
    /// Überschreiben der Append-Funktion
    /// </summary>
    /// <param name="loggingEvent">Daten des Events aus log4net</param>
    protected override void Append(LoggingEvent loggingEvent)
    {
        var message = RenderLoggingEvent(loggingEvent);
        m_StreamWriter.WriteLine(message);
    }

    /// <summary>
    /// FileStream-Instanz für die Ausgabe in eine Datei
    /// </summary>
    private FileStream m_FileStream;
    /// <summary>
    /// Instanz für das Schreiben in eine Datei.
    /// </summary>
    private readonly  StreamWriter m_StreamWriter;
}