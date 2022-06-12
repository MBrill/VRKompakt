using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Implementierung eines eigenen LogHandlers
/// </summary>
public class CustomLogHandler :  ILogHandler
{
    private FileStream m_FileStream;
    private readonly  StreamWriter m_StreamWriter;
    private readonly ILogHandler m_DefaultLogHandler = Debug.unityLogger.logHandler;

    /// <summary>
    /// Default-Konstruktor.
    /// <remarks>
    /// Wir schreiben die Datei loggingExample.csv
    /// in den dataPath der Anwendung.
    ///
    /// Im Editor ist dies das Verzeichnis Assets.
    /// M�glich w�re auch persistentDataPath,
    /// dann liegt das in Windows in AppData,
    /// wie die Playerlogs.
    /// </remarks>
    /// </summary>
    public CustomLogHandler()
    {
        var filePath = Application.dataPath + "/loggingExample.csv";

        m_FileStream = new FileStream(filePath, 
            FileMode.OpenOrCreate, 
            FileAccess.ReadWrite);
        m_StreamWriter = new StreamWriter(m_FileStream);

        // Den Default Handler durch diese Klasse ersetzen
        Debug.unityLogger.logHandler = this;
    }

    /// <summary>
    /// Wir �berschreiben LogFormat aus dem Interface.
    /// </summary>
    /// <param name="logType">Logging-Stufe</param>
    /// <param name="context">Welcher Kontext</param>
    /// <param name="format">Format-Angaben</param>
    /// <param name="args">Werte, die ausgegeben werden sollen</param>
    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        m_StreamWriter.WriteLine(String.Format(format, args));
        m_StreamWriter.Flush();
        m_DefaultLogHandler.LogFormat(logType, context, format, args);
    }

    /// <summary>
    /// �berschreiben der Funktion LogException im Interface
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="context"></param>
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        m_DefaultLogHandler.LogException(exception, context);
    }
}
